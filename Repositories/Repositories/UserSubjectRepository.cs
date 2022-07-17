using Contracts.Repositories;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Repositories
{
    public class UserSubjectRepository: RepositoryBase<UserSubject>, IUserSubjectRepository
    {
        public UserSubjectRepository(DataContext context) : base(context)
        {

        }
    }
}
