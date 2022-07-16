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
    public class SubjectGroupSubjectRepository : RepositoryBase<SubjectGroupSubject>, ISubjectGroupSubjectRepository
    {
        public SubjectGroupSubjectRepository(DataContext context) : base(context)
        {
        }

        public Task<List<SubjectReturn>> GetSubject(int group_id)
        {
            return FindByCondition(X => X.GroupSubjectId == group_id, false)
                .Include(X => X.Subject)
                .Select(X => new SubjectReturn
                {
                    Id = X.Subject.Id,
                    Name = X.Subject.Name
                }).ToListAsync();
        }
    }
}
