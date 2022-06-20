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

        public async Task<TestQuestions> GetMBTIQuestion(int id)
        {
            var result = await FindByCondition(x => x.QuestionId == id, false).FirstOrDefaultAsync();

            return result;
        }
    }
}
