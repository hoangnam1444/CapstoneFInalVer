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
    public class AnswerRepository : RepositoryBase<TestAnswers>, IAnswerRepository
    {
        public AnswerRepository(DataContext context) : base(context)
        {

        }

        public async Task<TestAnswers> GetAnswerById(int id)
        {
            return await FindByCondition(x => x.AnswerId == id, false).FirstOrDefaultAsync();
        }

        public async Task<List<AnswerInTest>> GetMbtiAnswer(int questionId)
        {
            var answers = await FindByCondition(x => x.QuestionId == questionId, false).ToListAsync();

            var result = answers.Select(x => new AnswerInTest
            {
                Content = x.AnswerContent,
                Id = x.AnswerId
            }).ToList();

            return result;
        }
    }
}
