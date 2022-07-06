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
    public class AnswerRepository : RepositoryBase<TestAnswers>, IAnswerRepository
    {
        public AnswerRepository(DataContext context) : base(context)
        {

        }

        public async Task<TestAnswers> GetAnswerById(int id)
        {
            return await FindByCondition(x => x.AnswerId == id, false).FirstOrDefaultAsync();
        }

        public async Task<List<HollandQuestion>> GetAnswerById(List<HollandQuestion> info)
        {
            var result = new List<HollandQuestion>();
            foreach (var question in info)
            {
                var answer = await FindByCondition(x => x.QuestionId == question.Id, false).ToListAsync();
                question.Answers = answer.Select(x => new AnswerInTest
                {
                    Id = x.AnswerId,
                    Content = x.AnswerContent
                }).ToList();
                result.Add(question);
            }
            return result;
        }

        public async Task<List<AnswerOfQuestion>> GetByQuestionId(int question_id)
        {
            var answers = await FindByCondition(x => x.QuestionId == question_id && x.IsDeleted == false, false).ToListAsync();
            return answers.Select(x => new AnswerOfQuestion
            {
                AnswerContent = x.AnswerContent,
                AnswerId = x.AnswerId,
                OrderIndex = x.OrderIndex,
            }).ToList();
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

        public async Task Update(int answer_id, UpdateAnswer info)
        {
            var answer = await FindByCondition(x => x.AnswerId == answer_id, true).FirstOrDefaultAsync();
            if (answer != null)
            {
                if (info.AnswerContent != null && info.AnswerContent != string.Empty)
                {
                    answer.AnswerContent = info.AnswerContent;
                }
                if (info.QuestionId > 0)
                {
                    answer.QuestionId = info.QuestionId;
                }
                if (info.OrderIndex > 0)
                {
                    answer.OrderIndex = info.OrderIndex;
                }
                Update(answer);
            }
        }
    }
}
