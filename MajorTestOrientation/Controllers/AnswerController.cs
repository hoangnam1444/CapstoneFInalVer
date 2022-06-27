using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/answers")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public AnswerController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        #region add answer
        [HttpPost]
        [Route("question/{question_id}")]
        public async Task<IActionResult> AddAnswer(NewAnswer info, int question_id)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }
            var ques = await _repository.Question.GetQuestionById(question_id);
            if (ques == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid question");
            var pers = await _repository.PersonalityGroup.GetById(info.PersonalityGroupId);
            if (pers == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid personality group");

            _repository.Answer.Create(new TestAnswers
            {
                AnswerContent = info.AnswerContent,
                IsDeleted = false,
                OrderIndex = info.OrderIndex,
                PersonalityGroupId = info.PersonalityGroupId,
                Point = info.Point,
                QuestionId = question_id
            });
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Save result from student
        /// <summary>
        /// Call to saving the test answer
        /// </summary>
        /// <param name="answer_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("result/{answer_id}")]
        public async Task<IActionResult> AddResult(int answer_id)
        {
            var userId = _userAccessor.GetAccountId();

            var answer = await _repository.Answer.GetAnswerById(answer_id);
            var question = await _repository.Question.GetQuestionById(answer.QuestionId);

            await _repository.TestResult.UpdateLastAnswer(answer_id, userId);
            _repository.TestResult.Create(new TestResults
            {
                IsLast = true,
                QuestionId = question.QuestionId,
                AnswerId = answer.AnswerId,
                TestId = question.TestId,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            });
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Get answer by question id
        [HttpGet]
        [Route("question/{question_id}")]
        public async Task<IActionResult> GetByQuestionId(int question_id)
        {
            var result = await _repository.Answer.GetByQuestionId(question_id);
            return Ok(result);
        }
        #endregion

        #region Update answer
        [HttpPut]
        [Route("{answer_id}")]
        public async Task<IActionResult> UpdateAnswer(int answer_id, UpdateAnswer info)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");

            if (info.QuestionId > 0)
            {
                var question = await _repository.Question.GetMBTIQuestion(info.QuestionId);
                if (question == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid question");
            }
            if (info.Point < 0)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid point");
            }

            await _repository.Answer.Update(answer_id, info);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion
    }
}
