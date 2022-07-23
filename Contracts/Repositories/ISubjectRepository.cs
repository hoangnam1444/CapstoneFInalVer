using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISubjectRepository
    {
        Task<string> GetName(List<int> savedPointSubjects);
    }
}