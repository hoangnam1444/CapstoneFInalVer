using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserMajorRepository : RepositoryBase<UserMajor>, IUserMajorRepository
    {
        public UserMajorRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<UserMajor>> GetMajorOfUser(int user_id)
        {
            var majors = await FindByCondition(x => x.UserId == user_id, false).ToListAsync();

            return majors;
        }

        public async Task<Pagination<StatisticMajor>> Statistic(PagingParameters param)
        {
            var majors = await FindAll(false).Include(x => x.Major).GroupBy(x => x.MajorId)
                .Select(x => new StatisticMajor
                {
                    MajorId = x.Key,
                    SelectedUser = x.Count(y => y.UserId > 0)
                }).ToListAsync();

            return new Pagination<StatisticMajor>
            {
                Count = majors.Count,
                Data = majors.Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize),
                PageSize = param.PageSize,
                PageNumber = param.PageNumber
            };
        }
    }
}
