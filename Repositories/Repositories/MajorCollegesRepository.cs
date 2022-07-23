using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MajorCollegesRepository : RepositoryBase<CollegeRefMajor>, IMajorCollegesRepository
    {
        public MajorCollegesRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<CollegesReturn>> GetColleges(int major_id)
        {
            var result = await FindByCondition(x => x.MajorId == major_id, false)
                .Include(x => x.Major)
                .Include(x => x.College)
                .Select(x => new CollegesReturn
                {
                    CollegeId = x.CollegeId,
                    CollegeName = x.College.CollegeName,
                    Address = x.College.Address,
                    ImagePath = x.College.ImagePath,
                    ReferenceLink = x.College.ReferenceLink,
                }).ToListAsync();
            return result;
        }

        public async Task<List<CollegesReturn>> GetMajor(List<CollegesReturn> result)
        {
            var finalResult = new List<CollegesReturn>();
            foreach(var college in result)
            {
                var majors = await FindByCondition(x => x.CollegeId == college.CollegeId, false)
                    .Include(x => x.Major)
                    .Select(x => new Major
                    {
                        Id = x.Major.MajorId,
                        Name = x.Major.MajorName
                    }).ToListAsync();
                college.Major = majors;
                finalResult.Add(college);
            }
            return finalResult;
        }

        public async Task<CollegesReturn> GetMajor(CollegesReturn college)
        {
            var majors = await FindByCondition(x => x.CollegeId == college.CollegeId, false)
                .Include(x => x.Major)
                .Select(x => new Major
                {
                    Id = x.Major.MajorId,
                    Name = x.Major.MajorName
                }).ToListAsync();
            college.Major = majors;

            return college;
        }

        public async Task<List<CollegesReturn>> GetSuggesionColleges(List<AttempData> finalData)
        {
            var result = new List<CollegesReturn>();
            foreach (var item in finalData)
            {
                var colleges = await FindByCondition(x => x.MajorId == item.MajorId, false)
                    .Include(x => x.College)
                    .Select(x => new CollegesReturn
                    {
                        Address = x.College.Address,
                        CollegeId = x.CollegeId,
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

        public async Task<List<CollegesReturn>> GetSuggesionColleges(int major_id)
        {
            var colleges = await FindByCondition(x => x.MajorId == major_id, false)
                .Include(x => x.College)
                .Select(x => new CollegesReturn
                {
                    Address = x.College.Address,
                    CollegeId = x.CollegeId,
                    CollegeName = x.College.CollegeName,
                    ImagePath = x.College.ImagePath,
                    ReferenceLink = x.College.ReferenceLink
                }).ToListAsync();

            return colleges;
        }
    }
}
