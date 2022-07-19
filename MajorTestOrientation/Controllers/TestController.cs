using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        /// <summary>
        /// Role: Admin (create test)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type_id"></param>
        /// <returns></returns>
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
        /// Role: Admin (Get all test)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("All")]
        public async Task<IActionResult> GetTestToUpdateQuestion([FromQuery]PagingParameters param)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }
            var result = await _repository.Test.GetAllTest(param);

            return Ok(result);
        }
        #endregion

        #region Get type of test
        /// <summary>
        /// Role: Admin (Get all type of test)
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
        /// <summary>
        /// Role: Admin (update type of test)
        /// </summary>
        /// <param name="type_id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Role: All (get test by its type)
        /// </summary>
        /// <param name="type_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("type/{type_id}")]
        public async Task<IActionResult> GetByTypeOfTest(int type_id)
        {
            var result = await _repository.Test.GetByType(type_id);
            return Ok(result);
        }
        #endregion

        #region Get personality result
        /// <summary>
        /// Role: Student (get personality group result)
        /// </summary>
        /// <param name="test_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("result/{test_id}")]
        public async Task<IActionResult> GetPersonalityGroupResult(int test_id)
        {
            var testResult = await _repository.TestResult.GetForPGroupResult(test_id, _userAccessor.GetAccountId());

            if(testResult.Count == 0 || testResult == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Don't have any result");
            }

            var pGroupPoint = await _repository.AnswerPGroup.GetPGroupResult(testResult);

            pGroupPoint = await _repository.PersonalityGroup.GetName(pGroupPoint);

            return Ok(pGroupPoint);
        }
        #endregion

        #region Get major result
        /// <summary>
        /// Role: Student (get major result)
        /// </summary>
        /// <param name="test_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{test_id}/major")]
        public async Task<IActionResult> GetMajorResult(int test_id)
        {
            var userId = _userAccessor.GetAccountId();

            var testResult = await _repository.TestResult.GetForPGroupResult(test_id, _userAccessor.GetAccountId());

            if (testResult.Count == 0 || testResult == null)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "Don't have any result");
            }

            var pGroupPoint = await _repository.AnswerPGroup.GetPGroupResult(testResult);

            var result = (List<MajorResult>)await _repository.MajorPgroup.GetMajorResult(pGroupPoint);

            return Ok(result);
        }
        #endregion

        #region Statistic personality group
        /// <summary>
        /// Role: Admin (Statistic personality group base on answer user submit)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("personality_gorup/statistic")]
        public async Task<IActionResult> StatisticPGroupResult()
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");

            var answersId = await _repository.TestResult.GetAllLastRecord();

            var result = await _repository.AnswerPGroup.GetStatistic(answersId);

            result = await _repository.PersonalityGroup.GetInfo(result);

            result.Sort(delegate(PGroupStatistic x, PGroupStatistic y) 
            {
                return x.AvgPoint > y.AvgPoint ? -1 : x.AvgPoint == y.AvgPoint ? 0 : 1;
            });

            return Ok(result);
        }
        #endregion

        #region Update test
        /// <summary>
        /// Role: Admin (update test declaration)
        /// </summary>
        /// <param name="test_id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Role: Admin (get test detail)
        /// </summary>
        /// <param name="test_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{test_id}/detail")]
        public async Task<IActionResult> GetDetail(int test_id)
        {
            var testD = await _repository.Test.GetById(test_id);

            var testType = await _repository.TestType.GetById(testD.TestTypeId);

            var result = new TestDetail
            {
                CreatedDate = testD.CreatedDate,
                TestType = new TestType
                {
                    TypeId = testType.TestTypeId,
                    TypeName = testType.TestTypeName
                },
                TestDescrip = testD.TestDescrip,
                TestId = testD.TestId
            };

            return Ok(result);
        }
    }
}
