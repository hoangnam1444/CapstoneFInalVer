using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SubjectGroupRepository : RepositoryBase<Entities.Models.SubjectGroup>, ISubjectGroupRepository
    {
        public SubjectGroupRepository(DataContext context) : base(context)
        {

        }

        public async Task<Pagination<Entities.DTOs.SubjectGroupReturn>> GetAll(PagingParameters param)
        {
            var result = await FindAll(true)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .Select(x => new SubjectGroupReturn
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToListAsync();

            return new Pagination<SubjectGroupReturn>
            {
                Count = await FindAll(true).CountAsync(),
                Data = result,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };
        }
    }
}
