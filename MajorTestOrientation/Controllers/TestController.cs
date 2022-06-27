using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/tests")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public TestController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Create test test
        [HttpPost]
        [Route("{type_id}")]
        public async Task<IActionResult> CreateTest(CreateTest info, int type_id)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            var type = await _repository.TestType.GetById(type_id);
            if (type == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid test type");

            _repository.Test.Create(new TestDeclarations
            {
                TestDescrip = info.TestDescrip,
                TestTypeId = type_id,
                CreatedDate = System.DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            });
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Get test for update question
        /// <summary>
        /// Show all test when admin UPDATE QUESTION (role: Admin)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetTestToUpdateQuestion()
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }
            var result = await _repository.Test.GetAllTest();

            return Ok(result);
        }
        #endregion

        #region Get type of test
        /// <summary>
        /// Get all type of test
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("type")]
        public async Task<IActionResult> GetTypeOfTest()
        {
            var result = await _repository.TestType.GetAllType();
            return Ok(result);
        }
        #endregion

        #region Update type of test
        [HttpPut]
        [Route("type/{type_id}")]
        public async Task<IActionResult> UpdateTypeOfTest(int type_id, UpdateType info)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            await _repository.TestType.Update(type_id, info);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

        #region Get by type id
        [HttpGet]
        [Route("type/{type_id}")]
        public async Task<IActionResult> GetByTypeOfTest(int type_id)
        {
            var result = await _repository.Test.GetByType(type_id);
            return Ok(result);
        }
        #endregion

        #region Get personality result
        [HttpGet]
        [Route("p_group/{test_id}")]
        public async Task<IActionResult> GetPersonalityGroupResult(int test_id)
        {
            var testResult = await _repository.TestResult.GetForPGroupResult(test_id, _userAccessor.GetAccountId());

            var pGroupPoint = await _repository.Answer.GetPGroupResult(testResult);

            pGroupPoint = await _repository.PersonalityGroup.GetName(pGroupPoint);

            return Ok(pGroupPoint);
        }
        #endregion

        #region Update test
        [HttpPut]
        [Route("{test_id}")]
        public async Task<IActionResult> UpdateTest(int test_id, UpdateTest info)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");

            if (info.TypeId > 0)
            {
                var type = await _repository.TestType.GetById(info.TypeId);
                if (type == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid test type");
            }

            await _repository.Test.Update(test_id, info);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion
    }
}
