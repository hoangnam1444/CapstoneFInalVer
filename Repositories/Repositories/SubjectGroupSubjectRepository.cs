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

        public async Task<List<SubjectGroupReturn>> GetSubjectOfGroup(List<SubjectGroupReturn> info)
        {
            var result = new List<SubjectGroupReturn>();
            foreach(var item in info)
            {
                var subjects = await FindByCondition(x => x.GroupSubjectId == item.Id, false)
                    .Include(x => x.Subject)
                    .Select(x => new SubjectReturn {Id = x.Subject.Id, Name = x.Subject.Name})
                    .ToListAsync();
                item.Subjects = subjects;
                result.Add(item);
            }
            return result;
        }

        public async Task<List<Subject>> GetSubjects(int groupId)
        {
            return await FindByCondition(x => x.GroupSubjectId == groupId, false)
                .Include(x => x.Subject)
                .Select(x => x.Subject)
                .ToListAsync();
        }
    }
}
