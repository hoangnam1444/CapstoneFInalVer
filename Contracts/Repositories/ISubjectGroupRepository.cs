using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISubjectGroupMajorRepository
    {
        Task<List<SubjectGroupReturn>> GetByMajor(int major_id);
    }
}
