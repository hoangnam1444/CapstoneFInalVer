using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
    public class UserSubjectGroupRepository : RepositoryBase<UserSubjectGroup>, IUserSubjectGroupRepository
    {
        public UserSubjectGroupRepository(DataContext context) : base (context)
        {

        }
    }
}
