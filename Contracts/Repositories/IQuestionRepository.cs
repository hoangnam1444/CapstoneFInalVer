using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IQuestionRepository
    {
        Task<List<int>> GetAllMbtiId();
        Task<TestQuestions> GetMBTIQuestion(int id);
        Task<TestQuestions> GetQuestionById(int id);
        Task<List<HollandQuestion>> GetHollandTest();
        Task<int> GetMaxIndex(int id);
        Task Update(int id, UpdateQuestion info);
        Task<List<QuestionOfTest>> GetByTestId(int test_id);
        void Create(TestQuestions question);
        Task<List<int>> GetForSavingAnswer(int test_id);
    }
}
