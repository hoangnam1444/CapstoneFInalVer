using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITestResultRepository
    {
        void Create(TestResults testResults);
        Task UpdateLastAnswer(int answerId, int userId);
    }
}
