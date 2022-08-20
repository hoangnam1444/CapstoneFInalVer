using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SubjectGroupMajorRepository : RepositoryBase<MajorSubjectGroup>, ISubjectGroupMajorRepository
    {
        public SubjectGroupMajorRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<SubjectGroupReturn>> GetByMajor(int major_id)
        {
            var list = await FindByCondition(x => x.MajorId == major_id, false)
                .Include(x => x.SubjectGroup)
                .Select(x => new SubjectGroupReturn
                {
                    Id = x.SubjectGroup.Id,
                    Name = x.SubjectGroup.Name
                }).ToListAsync();

            return list;
        }

        public async Task<List<int>> GetByMajor(List<UserMajor> majors)
        {
            var result = new List<int>();
            foreach (var major in majors)
            {
                var subjectGIds = await FindByCondition(x => x.MajorId == major.MajorId, false).Select(x => x.SubjectGroupId).ToListAsync();
                foreach (var id in subjectGIds)
                {
                    if (!result.Contains(id))
                    {
                        result.Add(id);
                    }
                }
            }
            return result;
        }

        public async Task<List<MajorSubjectGroup>> GetByMajorIds(List<UserMajor> major)
        {
            var majorIds = major.Select(x => x.MajorId).ToList();
            var result = await FindByCondition(x => majorIds.Contains(x.MajorId), false).ToListAsync();
            return result;
        }
    }
}
