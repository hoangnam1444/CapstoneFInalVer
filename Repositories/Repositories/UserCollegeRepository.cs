using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserCollegeRepository : RepositoryBase<UserColleges>,IUserCollegeRepository
    {
        public UserCollegeRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<CollegesReturn>> GetWishlist(int v)
        {
            var result = await FindByCondition(x => x.UserId == v, false)
                .Include(x => x.College)
                .Select(x => new CollegesReturn
                {
                    Address = x.College.Address,
                    CollegeId = x.CollegeId,
                    CollegeName = x.College.CollegeName,
                    ImagePath = x.College.ImagePath,
                    ReferenceLink = x.College.ReferenceLink
                }).ToListAsync();
            return result;
        }

        public async Task<Pagination<CollegesStatistic>> Statistic(PagingParameters param)
        {
            var data = await FindAll(false).GroupBy(x => x.CollegeId)
                .Select(x => new CollegesStatistic
                {
                    CollegeId = x.Key,
                    NumOfUser = x.Count(y => y.UserId > 0)
                }).ToListAsync();

            return new Pagination<CollegesStatistic>
            {
                Count = data.Count,
                Data = data.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize),
                PageSize = param.PageSize,
                PageNumber = param.PageNumber
            };
        }
    }
}