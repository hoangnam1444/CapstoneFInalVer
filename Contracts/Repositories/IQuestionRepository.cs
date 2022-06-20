using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IQuestionRepository
    {
        Task<TestQuestions> GetMBTIQuestion(int index);
    }
}
