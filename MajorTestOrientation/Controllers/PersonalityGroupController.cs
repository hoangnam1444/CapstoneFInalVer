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
        /// Role: Admin (Get all personality group)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllGroups([FromQuery] PagingParameters param)
        {
            var result = await _repository.PersonalityGroup.GetAllPGroup(param);
            return Ok(result);
        }
        #endregion

        #region Update personality group
        /// <summary>
        /// Role: Admin (Update personality group information)
        /// </summary>
        /// <param name="pgroup_id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{pgroup_id}")]
        public async Task<IActionResult> UpdatePGroup(int pgroup_id, UpdatePGroup info)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Not enough permission");

            if (info.TestTypeId > 0)
            {
                var testType = await _repository.TestType.GetById(info.TestTypeId);
                if (testType == null)
                    throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid test type");
            }

            await _repository.PersonalityGroup.Update(pgroup_id, info);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

        #region Get detail by id
        /// <summary>
        /// Role: User (Get personality group detail)
        /// </summary>
        /// <param name="pgroup_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{pgroup_id}/detail")]
        public async Task<IActionResult> GetDetailPerGroup(int pgroup_id)
        {
            var result = await _repository.PersonalityGroup.GetDetailById(pgroup_id);

            if (result == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Invalid Id or this group is deleted");

            return Ok(result);
        }
        #endregion
    }
}
