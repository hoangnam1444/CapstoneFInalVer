using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IUserSubjectGroupRepository
    {
        void Create(UserSubjectGroup userSubjectGroup);
    }
}
