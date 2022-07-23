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
    public class LessionMajorRepository : RepositoryBase<RecommentLession>, ILessionMajorRepository
    {
        public LessionMajorRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<LessionInList>> GetLessionbyListMajor(List<int> majorsId)
        {
            return await FindByCondition(x => majorsId.Contains(x.MajorId), false)
                .Include(x => x.Major)
                .Include(x => x.LessionDetail)
                .Select(x => new LessionInList
                {
                    Major = new Entities.DTOs.Majors
                    {
                        Id = x.Major.MajorId,
                        Name = x.Major.MajorName
                    },
                    Description = x.Description,
                    Detail = new Detail
                    {
                        Id = x.LessionDetail.LessionDetailId,
                        DetailContent = x.LessionDetail.LessionDetailContent,
                        Link = x.LessionDetail.Link
                    },
                    LessionId = x.LessionId
                })
                .ToListAsync();
        }
    }
}
