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
    public class CollegesRepository : RepositoryBase<Colleges>, ICollegesRepository
    {
        public CollegesRepository(DataContext context) : base (context)
        {
        }

        public async Task<Pagination<CollegesInList>> GetColleges(PagingParameters param)
        {
            var colleges = await FindByCondition(x => x.IsDeleted == false, false)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .Select(x => new CollegesInList
                {
                    Address = x.Address,
                    CollegeId = x.CollegeId,
                    CollegeName = x.CollegeName,
                    ImagePath = x.ImagePath,
                    ReferenceLink = x.ReferenceLink
                })
                .ToListAsync();

            return new Pagination<CollegesInList>
            {
                Data = colleges,
                Count = await FindByCondition(x => x.IsDeleted == false, false).CountAsync(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };
        }

        public async Task<CollegesReturn> GetDetail(int colleges_id)
        {
            var college = await FindByCondition(x => x.CollegeId == colleges_id, false)
                .Select(x => new CollegesReturn
                {
                    Address = x.Address,
                    CollegeId = x.CollegeId,
                    CollegeName = x.CollegeName,
                    ImagePath = x.ImagePath,
                    ReferenceLink = x.ReferenceLink
                })
                .FirstOrDefaultAsync();

            return college;
        }

        public async Task<Pagination<CollegesReturn>> GetAll(PagingParameters param)
        {
            var data = await FindByCondition(x => x.IsDeleted == false, false)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .Select(x => new CollegesReturn
                {
                    Address = x.Address,
                    CollegeId = x.CollegeId,
                    CollegeName = x.CollegeName,
                    ImagePath = x.ImagePath,
                    ReferenceLink = x.ReferenceLink
                }).ToListAsync();

            return new Pagination<CollegesReturn>
            {
                Count = await FindByCondition(x => x.IsDeleted == false, false).CountAsync(),
                Data = data,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };
        }


        public async Task<IEnumerable<CollegesStatistic>> GetName(IEnumerable<CollegesStatistic> data)
        {
            var result = new List<CollegesStatistic>();
            foreach(var college in data)
            {
                college.CollegeName = await FindByCondition(x => x.CollegeId == college.CollegeId, false).Select(x => x.CollegeName).FirstOrDefaultAsync();
                result.Add(college);
            }
            return result;
        }
    }
}
