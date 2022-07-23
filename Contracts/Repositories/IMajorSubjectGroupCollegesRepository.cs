using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorSubjectGroupCollegesRepository
    {
        Task<List<CollegesReturn>> GetSuggesionColleges(List<AttempData> finalData);
        Task<List<CollegesReturn>> GetSumPoint(List<CollegesReturn> result, List<AttempData> dataStudent);
        Task<List<CollegesReturn>> GetSumPoint(List<CollegesReturn> result);
    }
}
