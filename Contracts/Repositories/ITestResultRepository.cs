using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITestResultRepository
    {
        void Create(TestResults testResults);
        Task<List<TestResults>> GetForPGroupResult(int test_id, int userId);
        Task UpdateLastAnswer(int answerId, int userId);
        Task CreateResult(TestResults testResults);
        Task<List<int>> GetAllLastRecord();
    }
}
