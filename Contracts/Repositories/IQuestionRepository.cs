using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<int>> GetAllMbtiId();
        Task<TestQuestions> GetMBTIQuestion(int id);
        Task<List<HollandQuestion>> GetHollandTest();
    }
}
