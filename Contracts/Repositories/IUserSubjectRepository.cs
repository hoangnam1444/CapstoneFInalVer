using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserSubjectRepository
    {
        void Create(UserSubject userSub);
        Task<bool?> GetSavedSubject(int user_id, List<Subject> subjects);
        Task<GetCollegesData> GetSumOfSubjectGroup(List<Subject> subjects, int user_id);
    }
}
