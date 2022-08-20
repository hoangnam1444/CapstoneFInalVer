using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserCollegeRepository
    {
        void Create(UserColleges info);
        Task<List<CollegesReturn>> GetWishlist(int v);
        Task<Pagination<CollegesStatistic>> Statistic(PagingParameters param);
        Task<List<CollegesReturn>> GetSelectedUser(List<CollegesReturn> result, int v);
        Task<bool> SelectedColleges(int collegesId, int user_id);
        void Delete(UserColleges userColleges);
        Task<List<ChatBoxAccount>> GetConnector(int collegesId);
        Task<int> GetConColId(int receiverId);
    }
}