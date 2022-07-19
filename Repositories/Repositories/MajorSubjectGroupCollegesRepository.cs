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
    public class MajorSubjectGroupCollegesRepository : RepositoryBase<CollegesSubjectGroup>, IMajorSubjectGroupCollegesRepository
    {
        public MajorSubjectGroupCollegesRepository(DataContext context) : base(context)
        {

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
                    .Include(x => x.SubjectGroup)
                    .Include(x => x.Major)
                    .Include(x => x.College)
                    .Select(x => new CollegesReturn 
                    {
                        Address = x.College.Address,
                        CollegeId = x.CollegesId,
                        CollegeName = x.College.CollegeName,
                        ImagePath = x.College.ImagePath,
                        ReferenceLink = x.College.ReferenceLink,
                        Major = new Major { Id = x.MajorId, Name = x.Major.MajorName},
                        SubjectGroup = new Entities.DTOs.SubjectGroup { Name = x.SubjectGroup.Name, SumPoint = x.Sum}
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
    }
}
