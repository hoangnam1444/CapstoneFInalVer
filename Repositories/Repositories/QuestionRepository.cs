using Contracts.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class QuestionRepository : RepositoryBase<TestQuestions>, IQuestionRepository
    {
        public QuestionRepository(DataContext context) : base(context)
        {

        }

        public async Task<TestQuestions> GetMBTIQuestion(int index)
        {
            var result = await FindByCondition(x => x.OrderIndex == index, false).FirstOrDefaultAsync();

            return result;
        }
    }
}
