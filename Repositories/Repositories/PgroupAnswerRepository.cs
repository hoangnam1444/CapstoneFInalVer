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

        public async Task<List<PerGroup>> GetPGroupResult(List<TestResults> testResult)
        {
            var answersId = testResult.Select(x => x.AnswerId).ToList();

            var answer = await FindByCondition(x => answersId.Contains(x.AnswerId), false).GroupBy(x => x.PGroupId)
                .Select(x => new PerGroup
                {
                    AveragePoint = x.Average(y => y.Point),
                    Id = x.Key
                }).ToListAsync();

            return answer;
        }
    }
}
