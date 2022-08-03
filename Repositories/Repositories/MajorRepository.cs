using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Majors = Entities.Models.Majors;

namespace Repositories.Repositories
{
    public class MajorRepository : RepositoryBase<Majors>, IMajorRepository
    {
        public MajorRepository(DataContext context) : base(context)
        {

        }

        public async Task<Pagination<MajorResult>> GetAll(PagingParameters param)
        {
            var major = await FindAll(false).Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .Select(x => new MajorResult
                {
                    MajorName = x.MajorName,
                    MajorId = x.MajorId
                })
                .ToListAsync();
            return new Pagination<MajorResult>
            {
                PageSize = param.PageSize,
                PageNumber = param.PageNumber,
                Count = await FindAll(false).CountAsync(),
                Data = major
            };
        }

        public async Task<List<MajorForFilter>> GetAll()
        {
            return await FindAll(false).Select(x => new MajorForFilter
            {
                Id = x.MajorId,
                Name = x.MajorName
            }).ToListAsync();
        }

        public async Task<IEnumerable<StatisticMajor>> GetMajorName(IEnumerable<StatisticMajor> data)
        {
            var result = new List<StatisticMajor>();
            foreach (var major in data)
            {
                major.MajorName = await FindByCondition(x => x.MajorId == major.MajorId, false).Select(x => x.MajorName).FirstOrDefaultAsync();
                result.Add(major);
            }
            return result;
        }
    }
}