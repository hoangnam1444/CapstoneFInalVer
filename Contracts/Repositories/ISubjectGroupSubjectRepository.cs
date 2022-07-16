using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISubjectGroupSubjectRepository
    {
        Task<List<SubjectReturn>> GetSubject(int group_id);
    }
}
