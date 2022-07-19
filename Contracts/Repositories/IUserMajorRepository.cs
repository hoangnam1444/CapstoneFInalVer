using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserMajorRepository
    {
        void Create(UserMajor info);
        Task<List<UserMajor>> GetMajorOfUser(int user_id);
    }
}
