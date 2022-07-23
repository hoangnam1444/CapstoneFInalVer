using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorCollegesRepository
    {
        Task<List<CollegesReturn>> GetSuggesionColleges(List<AttempData> finalData);
        Task<List<CollegesReturn>> GetMajor(List<CollegesReturn> result);
        Task<List<CollegesReturn>> GetSuggesionColleges(int major_id);
    }
}
