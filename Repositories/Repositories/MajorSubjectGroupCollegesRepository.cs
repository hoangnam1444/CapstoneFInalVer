using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MajorSubjectGroupCollegesRepository : RepositoryBase<CollegesSubjectGroup>, IMajorSubjectGroupCollegesRepository
    {
        public MajorSubjectGroupCollegesRepository(DataContext context) : base(context)
        {

        }

        public async Task<CollegesSubjectGroup> GetPoint(PointCollege point)
        {
            var result = await FindByCondition(x => x.MajorId == point.MajorId && x.SubjectGroupId == point.SubjectGroupId && x.CollegesId == point.CollegesId, false)
                .FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<CollegesReturn>> GetSuggesionColleges(List<AttempData> finalData)
        {
            var result = new List<CollegesReturn>();
            foreach (var item in finalData)
            {
                var colleges = await FindByCondition(x => x.MajorId == item.MajorId
                && x.SubjectGroupId == item.Data.SubjectGroupId
                && x.Sum >= item.Data.Sum - 2
                && x.Sum <= item.Data.Sum + 2, false)
                    .Include(x => x.College)
                    .Select(x => new CollegesReturn 
                    {
                        Address = x.College.Address,
                        CollegeId = x.CollegesId,
                        CollegeName = x.College.CollegeName,
                        ImagePath = x.College.ImagePath,
                        ReferenceLink = x.College.ReferenceLink
                    })
                    .ToListAsync();
                foreach(var college in colleges)
                {
                    if (!result.Contains(college))
                    {
                        result.Add(college);
                    }
                }
            }
            return result;
        }

        public async Task<List<CollegesReturn>> GetSumPoint(List<CollegesReturn> result, List<AttempData> dataStudent)
        {
            var finalResult = new List<CollegesReturn>();

            foreach(var college in result)
            {
                var majors = new List<Major>();
                foreach(var major in college.Major)
                {
                    var subjectGroups = await FindByCondition(x => x.MajorId == major.Id
                    && x.CollegesId == college.CollegeId
                    && x.Sum >= (dataStudent.Where(y => y.MajorId == major.Id && y.Data.SubjectGroupId == x.SubjectGroupId).Select(y => y.Data.Sum).FirstOrDefault() - 2)
                    && x.Sum <= (dataStudent.Where(y => y.MajorId == major.Id && y.Data.SubjectGroupId == x.SubjectGroupId).Select(y => y.Data.Sum).FirstOrDefault() + 2), false)
                        .Include(x => x.SubjectGroup)
                        .Select(x => new Entities.DTOs.SubjectGroup
                        {
                            Name = x.SubjectGroup.Name,
                            SumPoint = x.Sum,
                            Id = x.SubjectGroupId
                        })
                        .ToListAsync();
                    major.SubjectGroup = subjectGroups;
                    majors.Add(major);
                }
                college.Major = majors;
                finalResult.Add(college);
            }
            return finalResult;
        }

        public async Task<List<CollegesReturn>> GetSumPoint(List<CollegesReturn> result)
        {
            var finalResult = new List<CollegesReturn>();

            foreach (var college in result)
            {
                var majors = new List<Major>();
                foreach (var major in college.Major)
                {
                    var subjectGroups = await FindByCondition(x => x.MajorId == major.Id
                    && x.CollegesId == college.CollegeId, false)
                        .Include(x => x.SubjectGroup)
                        .Select(x => new Entities.DTOs.SubjectGroup
                        {
                            Name = x.SubjectGroup.Name,
                            SumPoint = x.Sum,
                            Id = x.SubjectGroupId
                        })
                        .ToListAsync();
                    major.SubjectGroup = subjectGroups;
                    majors.Add(major);
                }
                college.Major = majors;
                finalResult.Add(college);
            }
            return finalResult;
        }

        public async Task<CollegesReturn> GetSumPoint(CollegesReturn college)
        {
            var majors = new List<Major>();
            foreach (var major in college.Major)
            {
                var subjectGroups = await FindByCondition(x => x.MajorId == major.Id
                && x.CollegesId == college.CollegeId, false)
                    .Include(x => x.SubjectGroup)
                    .Select(x => new Entities.DTOs.SubjectGroup
                    {
                        Name = x.SubjectGroup.Name,
                        SumPoint = x.Sum,
                        Id = x.SubjectGroupId
                    })
                    .ToListAsync();
                major.SubjectGroup = subjectGroups;
                majors.Add(major);
            }
            college.Major = majors;

            return college;
        }
    }
}
