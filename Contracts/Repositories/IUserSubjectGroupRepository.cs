using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserSubjectGroupRepository
    {
        void Create(UserSubjectGroup userSubjectGroup);
        Task<List<UserSubjectGroup>> GetSavedSubjectGroup(int user_id);
    }
}
