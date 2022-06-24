using Entities.DTOs;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPersonalityGroupRepository
    {
        Task<List<PerGroupReturn>> GetAllPGroup();
        Task Update(int pgroup_id, UpdatePGroup info);
    }
}
