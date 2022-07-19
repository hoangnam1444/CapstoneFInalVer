using Contracts.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserSubjectGroupRepository : RepositoryBase<UserSubjectGroup>, IUserSubjectGroupRepository
    {
        public UserSubjectGroupRepository(DataContext context) : base (context)
        {

        }

        public async Task<List<UserSubjectGroup>> GetSavedSubjectGroup(int user_id)
        {
            return await FindByCondition(x => x.UserId == user_id, false).ToListAsync();
        }
    }
}
