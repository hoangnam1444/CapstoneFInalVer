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
    }
}
