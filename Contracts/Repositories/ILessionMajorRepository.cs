using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ILessionMajorRepository
    {
        Task<List<LessionInList>> GetLessionbyListMajor(List<int> majorsId);
        Task<List<LessionInList>> GetAll();
    }
}
