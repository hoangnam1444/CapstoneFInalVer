using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public QuestionController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region Create test question
        /// <summary>
        /// Role: Admin (add new question to test)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="test_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{test_id}")]
        public async Task<IActionResult> CreateQuestion(CreateQuestion info, int test_id)
        {
            var role = _userAccessor.GetAccountRole();
            if(role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            var test = await _repository.Test.GetById(test_id);
            if (test == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid test");

            _repository.Question.Create(new TestQuestions
            {
                IsDeleted = false,
                QuestionContent = info.QuestionContent,
                TestId = test_id
            });
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

        #region Get MBTI test questionIdList
        /// <summary>
        /// Role: student (Get all id of mbti question)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all_mbti_id")]
        public async Task<IActionResult> GetAllMbtiQuestionId()
        {
            var result = await _repository.Question.GetAllMbtiId();
            return Ok(result);
        }
        #endregion

        #region Get MBTI test question
        /// <summary>
        /// Role: Student (Get mbti question)
        /// </summary>
        /// <param name="id">index of question</param>
        /// <returns></returns>
        [HttpGet]
        [Route("mbti/{id}")]
        public async Task<IActionResult> GetMBTIQuestion(int id)
        {
            var result = new MbtiQuestion();
            var question = await _repository.Question.GetMBTIQuestion(id);
            result.QuestionId = question.QuestionId;
            result.Content = question.QuestionContent;
            result.Index = question.OrderIndex;
            var answer = await _repository.Answer.GetMbtiAnswer(question.QuestionId);
            result.Answer = answer;
            return Ok(result);
        }
        #endregion

        #region Get Holland test
        /// <summary>
        /// Role: Student (Get all holland question)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("holland")]
        public async Task<IActionResult> GetHollandTest()
        {
            var result = await _repository.Question.GetHollandTest();
            result = await _repository.Answer.GetAnswerById(result);
            return Ok(result);
        }
        #endregion

        #region Get question by test id
        /// <summary>
        /// Role: Admin (Get question by test id)
        /// </summary>
        /// <param name="param"></param>
        /// <param name="test_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{test_id}")]
        public async Task<IActionResult> GetByTestId([FromQuery]PagingParameters param, int test_id)
        {
            var result = await _repository.Question.GetByTestId(test_id, param);
            return Ok(result);
        }
        #endregion

        #region Update question
        /// <summary>
        /// Role: Admin (update question)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="question_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{question_id}")]
        public async Task<IActionResult> UpdateQuestion(UpdateQuestion info, int question_id)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            if (info.TestId > 0)
            {
                var test = await _repository.Test.GetById(info.TestId);
                if (test == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid test id");
            }
            if (info.QuestionContent != string.Empty && info.QuestionContent != null)
            {
                if (info.QuestionContent.Length > 500)
                    throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Over length of question content");
            }
            if (info.OrderIndex > 0)
            {
                var maxIndex = await _repository.Question.GetMaxIndex(question_id);
                if (info.OrderIndex > maxIndex)
                {
                    throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, string.Format($"Order index must be from 0 to {0}", maxIndex));
                }
            }

            await _repository.Question.Update(question_id, info);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        [HttpGet]
        [Route("{question_id}/detail")]
        public async Task<IActionResult> GetDetail(int question_id)
        {
            var result = await _repository.Question.GetDetail(question_id);

            return Ok(result);
        }
    }
}
