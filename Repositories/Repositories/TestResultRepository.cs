using Contracts.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class TestResultRepository : RepositoryBase<TestResults>, ITestResultRepository
    {
        public TestResultRepository(DataContext context) : base(context)
        {

        }

        public async Task CreateResult(TestResults testResults)
        {
            var savedResult = await FindByCondition(x => x.AnswerId == testResults.AnswerId
            && x.TestId == testResults.TestId
            && x.QuestionId == testResults.QuestionId
            && x.UserId == testResults.UserId, true).ToListAsync();

            if(savedResult != null && savedResult.Count > 0)
            {
                foreach(var result in savedResult)
                {
                    result.IsLast = false;
                    Update(result);
                }
            }

            Create(testResults);
        }

        public async Task<List<TestResults>> GetForPGroupResult(int test_id, int userId)
        {
            return await FindByCondition(x => x.TestId == test_id && x.UserId == userId && x.IsLast == true, false).ToListAsync();
        }

        public async Task UpdateLastAnswer(int answerId, int userId)
        {
            var lastResult = await FindByCondition(x => x.AnswerId == answerId && x.UserId == userId, true).FirstOrDefaultAsync();

            if (lastResult != null)
            {
                lastResult.IsLast = false;
                Update(lastResult);
            }
        }
    }
}
