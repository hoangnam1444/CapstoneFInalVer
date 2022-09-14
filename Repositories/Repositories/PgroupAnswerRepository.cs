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
    public class PgroupAnswerRepository : RepositoryBase<AnswersPGroups>, IPgroupAnswerRepository
    {
        public PgroupAnswerRepository(DataContext context) : base(context)
        {

        }

        public async Task<AnswerDetail> GetAnswerDetail(int answer_id)
        {
            var answerPGroup = await FindByCondition(x => x.AnswerId == answer_id, false).Include(x => x.Answer).Include(x => x.PGroup).ToListAsync();

            if (answerPGroup.Count == 0) return null;

            var pGroupList = answerPGroup.Select(x => new PGroupOfAnswer
            {
                Name = x.PGroup.PersonalityGroupName,
                PGroupId = x.PGroupId,
                Point = x.Point
            }).ToList();

            var answer = new AnswerOfQuestion
            {
                AnswerContent = answerPGroup[0].Answer.AnswerContent,
                AnswerId = answerPGroup[0].AnswerId,
                OrderIndex = answerPGroup[0].Answer.OrderIndex
            };
            return new AnswerDetail
            {
                Answer = answer,
                PeronalityGroups = pGroupList
            };
        }

        public async Task<List<PerGroup>> GetPGroupResult(List<TestResults> testResult)
        {
            var answersId = testResult.Select(x => x.AnswerId).ToList();

            var answer = await FindByCondition(x => answersId.Contains(x.AnswerId), false).GroupBy
                (x => x.PGroupId)
                .Select(x => new PerGroup
                {
                    AveragePoint = x.Average(y => y.Point),
                    Id = x.Key
                }).ToListAsync();

            return answer;
        }

        public async Task<List<PGroupStatistic>> GetStatistic(List<int> answerIds)
        {
            var answer = await FindByCondition(x => answerIds.Contains(x.AnswerId), false).Include(x => x.PGroup).GroupBy(x => x.PGroupId)
                .Select(x => new PGroupStatistic
                {
                    AvgPoint = x.Average(y => y.Point),
                    GroupId = x.Key
                }).ToListAsync();

            return answer;
        }
    }
}
