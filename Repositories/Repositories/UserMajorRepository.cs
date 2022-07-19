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
    public class UserMajorRepository : RepositoryBase<UserMajor>, IUserMajorRepository
    {
        public UserMajorRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<UserMajor>> GetMajorOfUser(int user_id)
        {
            var majors = await FindByCondition(x => x.UserId == user_id, false).ToListAsync();

            return majors;
        }
    }
}
