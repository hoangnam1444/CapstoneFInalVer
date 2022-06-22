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
    public class TestDeclarationRepository : RepositoryBase<TestDeclarations>, ITestDeclarationRepository
    {
        public TestDeclarationRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<TestToUpdateQuestion>> GetAllTest()
        {
            var allTest = await FindAll(false).ToListAsync();

            return allTest.Select(x => new TestToUpdateQuestion { Id = x.TestId, TestDescript = x.TestDescrip }).ToList();
        }

        public async Task<TestDeclarations> GetById(int id)
        {
            return await FindByCondition(x => x.TestId == id, false).FirstOrDefaultAsync();
        }
    }
}
