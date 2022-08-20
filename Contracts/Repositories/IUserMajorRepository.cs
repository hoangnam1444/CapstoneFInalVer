using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserMajorRepository
    {
        void Create(UserMajor info);
        Task<List<UserMajor>> GetMajorOfUser(int user_id);
        Task<Pagination<StatisticMajor>> Statistic(PagingParameters param);
    }
}
