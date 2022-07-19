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
    public class UserSubjectRepository: RepositoryBase<UserSubject>, IUserSubjectRepository
    {
        public UserSubjectRepository(DataContext context) : base(context)
        {

        }

        public async Task<bool?> GetSavedSubject(int user_id, List<Subject> subjects)
        {
            foreach(var subject in subjects)
            {
                var savedPoint = await FindByCondition(x => x.UserId == user_id && x.SubjectId == subject.Id, false)
                    .FirstOrDefaultAsync();
                if (savedPoint == null) return null;
                else if (savedPoint.Point == 0) return false;
            }
            return true;
        }

        public async Task<GetCollegesData> GetSumOfSubjectGroup(List<Subject> subjects, int user_id)
        {
            var subjectIds = subjects.Select(x => x.Id);
            var result = await FindByCondition(x => x.UserId == user_id && subjectIds.Contains(x.SubjectId), false)
                .GroupBy(x => x.UserId)
                .Select(x => new GetCollegesData
                {
                    Sum = x.Sum(y => y.Point),
                    UserId = user_id
                }).FirstOrDefaultAsync();

            return result;
        }
    }
}
