using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorCollegesRepository
    {
        Task<List<CollegesReturn>> GetColleges(int major_id);
    }
}
