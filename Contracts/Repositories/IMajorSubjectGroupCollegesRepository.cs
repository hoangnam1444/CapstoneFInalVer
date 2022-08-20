using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorSubjectGroupCollegesRepository
    {
        Task<List<CollegesReturn>> GetSuggesionColleges(List<AttempData> finalData);
        Task<List<CollegesReturn>> GetSumPoint(List<CollegesReturn> result, List<AttempData> dataStudent);
        Task<List<CollegesReturn>> GetSumPoint(List<CollegesReturn> result);
        Task<CollegesReturn> GetSumPoint(CollegesReturn college);
        void Create(CollegesSubjectGroup collegesSubjectGroup);
        Task<CollegesSubjectGroup> GetPoint(PointCollege point);
        void Update(CollegesSubjectGroup updateInfo);
    }
}
