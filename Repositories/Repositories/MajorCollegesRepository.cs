using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MajorCollegesRepository : RepositoryBase<CollegeRefMajor>, IMajorCollegesRepository
    {
        public MajorCollegesRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<CollegesReturn>> GetColleges(int major_id)
        {
            var result = await FindByCondition(x => x.MajorId == major_id, false)
                .Include(x => x.College)
                .Select(x => new CollegesReturn
                {
                    CollegeId = x.CollegeId,
                    CollegeName = x.College.CollegeName,
                    Address = x.College.Address,
                    ImagePath = x.College.ImagePath,
                    ReferenceLink = x.College.ReferenceLink
                }).ToListAsync();
            return result;
        }
    }
}
