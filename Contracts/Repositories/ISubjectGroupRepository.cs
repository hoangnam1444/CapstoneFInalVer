using Entities.DTOs;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISubjectGroupMajorRepository
    {
        Task<List<SubjectGroupReturn>> GetByMajor(int major_id);
        Task<List<int>> GetByMajor(List<UserMajor> majors);
        Task<List<MajorSubjectGroup>> GetByMajorIds(List<UserMajor> major);
    }
}
