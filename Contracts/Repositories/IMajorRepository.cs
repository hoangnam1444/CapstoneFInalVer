using Entities.DTOs;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorRepository
    {
        Task<List<MajorResult>> GetAll(PagingParameters param);
    }
}