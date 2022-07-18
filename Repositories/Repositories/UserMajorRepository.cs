using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
    public class UserMajorRepository : RepositoryBase<UserMajor>, IUserMajorRepository
    {
        public UserMajorRepository(DataContext context) : base(context)
        {

        }
    }
}
