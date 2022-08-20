using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class LessionMajorRepository : RepositoryBase<RecommentLession>, ILessionMajorRepository
    {
        public LessionMajorRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<LessionInList>> GetAll()
        {
            return await FindAll(false)
                .Include(x => x.Major)
                .Select(x => new LessionInList
                {
                    Major = new Entities.DTOs.Majors
                    {
                        Id = x.Major.MajorId,
                        Name = x.Major.MajorName
                    },
                    Description = x.LessionDetailContent,
                    LessionId = x.LessionId,
                    Link = x.Link,
                    ImageUrl = x.ImageUrl
                })
                .ToListAsync();
        }

        public async Task<List<LessionInList>> GetLessionbyListMajor(List<int> majorsId)
        {
            return await FindByCondition(x => majorsId.Contains(x.MajorId), false)
                .Include(x => x.Major)
                .Select(x => new LessionInList
                {
                    Major = new Entities.DTOs.Majors
                    {
                        Id = x.Major.MajorId,
                        Name = x.Major.MajorName
                    },
                    Description = x.LessionDetailContent,
                    LessionId = x.LessionId,
                    Link = x.Link
                })
                .ToListAsync();
        }
    }
}
