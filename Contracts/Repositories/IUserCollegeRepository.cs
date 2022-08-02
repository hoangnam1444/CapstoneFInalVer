using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserCollegeRepository
    {
        void Create(UserColleges info);
        Task<List<CollegesReturn>> GetWishlist(int v);
    }
}