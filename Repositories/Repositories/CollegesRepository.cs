using Contracts.Repositories;
using Entities;
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

        public async Task<List<CollegesInList>> GetColleges(PagingParameters param)
        {
            var colleges = await FindAll(false)
                .Skip((param.PageNumber - 1) - param.PageSize)
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

            return colleges;
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
    }
}
