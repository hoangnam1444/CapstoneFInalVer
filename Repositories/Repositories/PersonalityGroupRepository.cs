using Contracts.Repositories;
using Entities;
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

        public async Task<List<PerGroupReturn>> GetAllPGroup()
        {
            var pGroups = await FindByCondition(x => x.IsDeleted == false, false).ToListAsync();
            return pGroups.Select(x => new PerGroupReturn
            {
                PersonalityGroupId = x.PersonalityGroupId,
                PersonalityGroupName = x.PersonalityGroupName,
                TestTypeId = x.TestTypeId
            }).ToList();
        }

        public async Task<TestPersonalityGroups> GetById(int id)
        {
            return await FindByCondition(x => x.PersonalityGroupId == id && x.IsDeleted == false, false).FirstOrDefaultAsync();
        }

        public async Task Update(int pgroup_id, UpdatePGroup info)
        {
            var pGroup = await FindByCondition(x => x.PersonalityGroupId == pgroup_id, true).FirstOrDefaultAsync();

            if(info.TestTypeId > 0)
            {
                pGroup.TestTypeId = info.TestTypeId;
            }
            if(info.PersonalityGroupName != string.Empty && info.PersonalityGroupName != null)
            {
                pGroup.PersonalityGroupName = info.PersonalityGroupName;
            }
            Update(pGroup);
        }
    }
}
