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
    public class MajorPGroupRepository : RepositoryBase<MajorRefPersonality>, IMajorPGroupRepository
    {
        public MajorPGroupRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<MajorResult>> GetByGroupId(int group_id)
        {
            var majors = await FindByCondition(x => x.PersonalityGroupId == group_id, false)
                .Include(x => x.Major)
                .Select(x => new MajorResult
                {
                    MajorId = x.MajorId,
                    Description = x.Major.MajorDetail,
                    MajorName = x.Major.MajorName
                })
                .ToListAsync();

            return majors;
        }

        public async Task<List<MajorResult>> GetMajorResult(List<PerGroup> pGroupPoint)
        {
            var result = new List<MajorResult>();
            var max = (double)0;
            foreach(var group in pGroupPoint)
            {
                if (group.AveragePoint > max)
                {
                    max = group.AveragePoint;
                    var major = await FindByCondition(x => x.PersonalityGroupId == group.Id, false).Include(x => x.Major).ToListAsync();
                    result = major.Select(x => new MajorResult
                    {
                        MajorId = x.MajorId,
                        MajorName = x.Major.MajorName,
                        Description = x.Major.MajorDetail
                    }).ToList();
                }
            }
            return result;
        }
    }
}
