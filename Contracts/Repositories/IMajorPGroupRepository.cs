using Entities.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IMajorPGroupRepository
    {
        Task<List<MajorResult>> GetMajorResult(List<PerGroup> pGroupPoint);
    }
}
