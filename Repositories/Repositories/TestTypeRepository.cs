using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class TestTypeRepository : RepositoryBase<TestTypes>, ITestTypeRepository
    {
        public TestTypeRepository(DataContext context) : base(context)
        {

        }
        public async Task<List<TypeForGetAll>> GetAllType()
        {
            var types = await FindByCondition(x => x.IsDeleted == false, false).ToListAsync();
            return types.Select(x => new TypeForGetAll { Id = x.TestTypeId, Name = x.TestTypeName }).ToList();
        }

        public async Task<TestTypes> GetById(int typeId)
        {
            return await FindByCondition(x => x.TestTypeId == typeId, false).FirstOrDefaultAsync();
        }

        public async Task Update(int type_id, UpdateType info)
        {
            var type = await FindByCondition(x => x.TestTypeId == type_id, true).FirstOrDefaultAsync();
            if(type != null)
            {
                type.TestTypeName = info.Name;
                Update(type);
            }
        }
    }
}
