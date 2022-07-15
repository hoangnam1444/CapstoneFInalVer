using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IPersonalityGroupRepository
    {
        Task<Pagination<PerGroupResult>> GetAllPGroup(PagingParameters param);
        Task Update(int pgroup_id, UpdatePGroup info);
        Task<TestPersonalityGroups> GetById(int id);
        Task<List<PerGroup>> GetName(List<PerGroup> pGroupPoint);
        Task<PGroupDetail> GetDetailById(int id);
        Task<List<PGroupStatistic>> GetInfo(List<PGroupStatistic> result);
    }
}
