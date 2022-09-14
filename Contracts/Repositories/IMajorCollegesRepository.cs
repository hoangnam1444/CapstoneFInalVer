using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorCollegesRepository
    {
        Task<List<CollegesReturn>> GetSuggesionColleges(List<AttempData> finalData);
        Task<List<CollegesReturn>> GetMajor(List<CollegesReturn> result);
        Task<List<CollegesReturn>> GetSuggesionColleges(int major_id);
        Task<CollegesReturn> GetMajor(CollegesReturn college);
        void Create(CollegeRefMajor collegeRefMajor);
        void Delete(CollegeRefMajor collegeRefMajor);
        Task<List<MajorCD>> GetByColleges(int college_id);
    }
}
