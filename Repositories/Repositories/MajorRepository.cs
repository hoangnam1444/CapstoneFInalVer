using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class MajorRepository : RepositoryBase<Major>,IMajorRepository
    {
        public MajorRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<MajorResult>> GetAll(PagingParameters param)
        {
            var major = await FindAll(false).Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .Select(x => new MajorResult
                {
                    MajorName = x.Name,
                    MajorId = x.Id
                })
                .ToListAsync();
            return major;
        }
    }
}