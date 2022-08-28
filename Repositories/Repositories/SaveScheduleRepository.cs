using Contracts.Repositories;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SaveScheduleRepository : RepositoryBase<SavedSchedule>, ISaveScheduleRepository
    {
        public SaveScheduleRepository(DataContext context) : base(context)
        {

        }

        public async Task<SavedSchedule> GetByDay(int day)
        {
            return await FindByCondition(x => x.Day == day, false).FirstOrDefaultAsync();
        }
    }
}
