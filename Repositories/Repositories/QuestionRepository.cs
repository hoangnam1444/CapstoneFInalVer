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
    public class QuestionRepository : RepositoryBase<TestQuestions>, IQuestionRepository
    {
        public QuestionRepository(DataContext context) : base(context)
        {

        }

        public async Task<List<int>> GetAllMbtiId()
        {
            var questions = await FindByCondition(x => x.TestId == 1, false).OrderBy(x => x.OrderIndex).ToListAsync();
            var result = questions.Select(x => x.QuestionId).ToList();
            return result;
        }

        public async Task<List<QuestionOfTest>> GetByTestId(int test_id)
        {
            var question = await FindByCondition(x => x.TestId == test_id, false).ToListAsync();
            return question.Select(x => new QuestionOfTest { OrderIndex = x.OrderIndex, QuestionContent = x.QuestionContent, QuestionId = x.QuestionId })
                .ToList();
        }

        public async Task<List<HollandQuestion>> GetHollandTest()
        {
            var questions = await FindByCondition(x => x.TestId == 2, false).ToListAsync();
            var result = questions.Select(x => new HollandQuestion
            {
                content = x.QuestionContent,
                Id = x.QuestionId,
                Indext = x.OrderIndex
            }).ToList();
            return result;
        }

        public async Task<int> GetMaxIndex(int id)
        {
            var question = await FindByCondition(x => x.QuestionId == id, false).FirstOrDefaultAsync();
            return await FindByCondition(x => x.TestId == question.TestId, false).CountAsync();
        }

        public async Task<TestQuestions> GetMBTIQuestion(int id)
        {
            var result = await FindByCondition(x => x.QuestionId == id, false).FirstOrDefaultAsync();

            return result;
        }

        public Task<TestQuestions> GetQuestionById(int id)
        {
            return FindByCondition(x => x.QuestionId == id, false).FirstOrDefaultAsync();
        }

        public async Task Update(int id, UpdateQuestion info)
        {
            var question = await FindByCondition(x => x.QuestionId == id, true).FirstOrDefaultAsync();
            if (info.TestId > 0)
            {
                question.TestId = info.TestId;
            }
            if (info.QuestionContent != string.Empty && info.QuestionContent != null)
            {
                question.QuestionContent = info.QuestionContent;
            }
            if (info.OrderIndex > 0)
            {
                question.OrderIndex = info.OrderIndex;
            }
            Update(question);
        }
    }
}
