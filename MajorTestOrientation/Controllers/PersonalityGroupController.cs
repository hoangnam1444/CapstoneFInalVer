using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/personality_groups")]
    [ApiController]
    public class PersonalityGroupController : ControllerBase
    {
        private readonly IUserAccessor _userAccessor;
        private readonly IRepositoryManager _repository;

        public PersonalityGroupController(IUserAccessor userAccessor, IRepositoryManager repository)
        {
            _userAccessor = userAccessor;
            _repository = repository;
        }

        #region Get all p group
        /// <summary>
        /// Get all personality group
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGroups()
        {
            var result = await _repository.PersonalityGroup.GetAllPGroup();
            return Ok(result);
        }
        #endregion

        #region Update personality group
        [HttpPut]
        [Route("{pgroup_id}")]
        public async Task<IActionResult> UpdatePGroup(int pgroup_id, UpdatePGroup info)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Not enough permission");

            if(info.TestTypeId > 0)
            {
                var testType = _repository.TestType.GetById(info.TestTypeId);
                if (testType == null)
                    throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid test type");
            }

            await _repository.PersonalityGroup.Update(pgroup_id, info);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion
    }
}
