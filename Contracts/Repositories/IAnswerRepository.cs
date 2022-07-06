using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IAnswerRepository
    {
        Task<List<AnswerInTest>> GetMbtiAnswer(int questionId);
        Task<TestAnswers> GetAnswerById(int id);
        Task<List<AnswerOfQuestion>> GetByQuestionId(int question_id);
        Task Update(int answer_id, UpdateAnswer info);
        void Create(TestAnswers answer);
        Task<List<HollandQuestion>> GetAnswerById(List<HollandQuestion> result);
    }
}
