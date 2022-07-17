using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts.Repositories
{
    public interface IUserSubjectRepository
    {
        void Create(UserSubject userSub);
    }
}
