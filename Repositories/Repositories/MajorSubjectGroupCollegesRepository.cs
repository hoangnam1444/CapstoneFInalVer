using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MajorSubjectGroupCollegesRepository : RepositoryBase<CollegesSubjectGroup>, IMajorSubjectGroupCollegesRepository
    {
        public MajorSubjectGroupCollegesRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<MajorCD>> GetcDetail(List<MajorCD> majors)
        {
            var result = new List<MajorCD>();

            foreach(var major in majors)
            {
                major.SubjectGroups = await FindByCondition(x => x.MajorId == major.MajorId, true)
                    .Include(x => x.SubjectGroup)
                    .Select(x => new Entities.DTOs.SubjectGroup { Id = x.SubjectGroupId, Name = x.SubjectGroup.Name, SumPoint = x.Sum })
                    .ToListAsync();
                result.Add(major);
            }

            return result;
        }

        public async Task<List<ListColleges>> GetCollegesDash(List<MajorForFilter> majors)
        {
            var result = new List<ListColleges>();
            foreach(var major in majors)
            {
                var college = await FindByCondition(x => x.MajorId == major.Id, false)
                    .Include(x => x.College)
                    .Include(x => x.Major)
                    .Include(x => x.SubjectGroup)
                    .MaxAsync();

                var data = new ListColleges
                {
                    CollegesId = college.CollegesId,
                    Image = college.College.ImagePath,
                    MajorName = college.Major.MajorName,
                    Name = college.College.CollegeName,
                    RefLink = college.College.ReferenceLink,
                    SubjectGroup = college.SubjectGroup.Name,
                    Sum = college.Sum
                };
                result.Add(data);
            }
            return result;
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
                foreach (var college in colleges)
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

            foreach (var college in result)
            {
                var majors = new List<Major>();
                foreach (var major in college.Major)
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
