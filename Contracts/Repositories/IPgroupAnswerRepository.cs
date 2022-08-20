using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPgroupAnswerRepository
    {
        Task<List<PerGroup>> GetPGroupResult(List<TestResults> testResult);
        Task<List<PGroupStatistic>> GetStatistic(List<int> answerIds);
        Task<AnswerDetail> GetAnswerDetail(int answer_id);
        void Create(AnswersPGroups answersPGroups);
    }
}
