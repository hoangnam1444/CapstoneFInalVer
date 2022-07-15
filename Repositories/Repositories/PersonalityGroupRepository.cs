using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class PersonalityGroupRepository : RepositoryBase<TestPersonalityGroups>, IPersonalityGroupRepository
    {
        public PersonalityGroupRepository(DataContext context) : base(context)
        {

        }

        public async Task<Pagination<PerGroupResult>> GetAllPGroup(PagingParameters param)
        {
            var pGroups = await FindByCondition(x => x.IsDeleted == false, false)
                .Skip((param.PageNumber - 1) * param.PageSize)
                .Take(param.PageSize)
                .ToListAsync();

            return new Pagination<PerGroupResult>
            {
                Count = await FindByCondition(x => x.IsDeleted == false, false).CountAsync(),
                Data = pGroups.Select(x => new PerGroupResult
                {
                    PersonalityGroupId = x.PersonalityGroupId,
                    PersonalityGroupName = x.PersonalityGroupName,
                    TestTypeId = x.TestTypeId
                }).ToList(),
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };
        }

        public async Task<TestPersonalityGroups> GetById(int id)
        {
            return await FindByCondition(x => x.PersonalityGroupId == id && x.IsDeleted == false, false).FirstOrDefaultAsync();
        }

        public async Task<PGroupDetail> GetDetailById(int id)
        {
            var pGroup = await FindByCondition(x => x.PersonalityGroupId == id && x.IsDeleted == false, false).FirstOrDefaultAsync();

            if (pGroup == null) return null;

            return new PGroupDetail
            {
                Description = pGroup.Description,
                Id = pGroup.PersonalityGroupId,
                Name = pGroup.PersonalityGroupName
            };
        }

        public async Task<List<PGroupStatistic>> GetInfo(List<PGroupStatistic> input)
        {
            var result = new List<PGroupStatistic>();

            foreach(var item in input)
            {
                var pGroup = await FindByCondition(x => x.PersonalityGroupId == item.GroupId, false).FirstOrDefaultAsync();

                item.GroupName = pGroup.PersonalityGroupName;
                item.Description = pGroup.Description;

                result.Add(item);
            }
            return result;
        }

        public async Task<List<PerGroup>> GetName(List<PerGroup> pGroupPoint)
        {
            var result = new List<PerGroup>();
            foreach (var group in pGroupPoint)
            {
                var pGroup = await FindByCondition(x => x.PersonalityGroupId == group.Id, false).FirstOrDefaultAsync();
                group.Name = pGroup.PersonalityGroupName;
                group.Description = pGroup.Description;
                result.Add(group);
            }
            return result;
        }

        public async Task Update(int pgroup_id, UpdatePGroup info)
        {
            var pGroup = await FindByCondition(x => x.PersonalityGroupId == pgroup_id, true).FirstOrDefaultAsync();

            if (info.TestTypeId > 0)
            {
                pGroup.TestTypeId = info.TestTypeId;
            }
            if (info.PersonalityGroupName != string.Empty && info.PersonalityGroupName != null)
            {
                pGroup.PersonalityGroupName = info.PersonalityGroupName;
            }
            Update(pGroup);
        }
    }
}
