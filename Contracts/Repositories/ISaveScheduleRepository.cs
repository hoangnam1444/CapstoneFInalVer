using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISaveScheduleRepository
    {
        Task<SavedSchedule> GetByDay(int day);
        void Create(SavedSchedule savedSchedule);
    }
}