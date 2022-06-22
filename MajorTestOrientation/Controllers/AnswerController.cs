using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.Models;
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
    }
}
