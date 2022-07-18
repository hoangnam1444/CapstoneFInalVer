using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
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

        public async Task<Pagination<QuestionOfTest>> GetByTestId(int test_id, PagingParameters param)
        {
            var question = await FindByCondition(x => x.TestId == test_id, false)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToListAsync();
            return new Pagination<QuestionOfTest>
            {
                Count = await FindByCondition(x => x.TestId == test_id, false).CountAsync(),
                Data = question.Select(x => new QuestionOfTest { OrderIndex = x.OrderIndex, QuestionContent = x.QuestionContent, QuestionId = x.QuestionId })
                .ToList(),
                PageSize = param.PageSize,
                PageNumber = param.PageNumber
            };   
        }

        public async Task<QuestionOfTest> GetDetail(int question_id)
        {
            var result = await FindByCondition(x => x.QuestionId == question_id, false)
                .Select(x => new QuestionOfTest 
                {
                    QuestionId = x.QuestionId,
                    OrderIndex = x.OrderIndex,
                    QuestionContent = x.QuestionContent
                }).FirstOrDefaultAsync();
            return result;
        }

        public async Task<List<int>> GetForSavingAnswer(int test_id)
        {
            return await FindByCondition(x => x.TestId == test_id, false).Select(x => x.QuestionId).ToListAsync();
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
