using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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
        /// <summary>
        /// Role: Admin (For adding answer to test)
        /// </summary>
        /// <param name="info"></param>
        /// <param name="question_id"></param>
        /// <returns></returns>
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
            if (info.PersonalityGroupId.Count == 0) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Answer must have at least one personality group");
            foreach (var id in info.PersonalityGroupId)
            {
                var pers = await _repository.PersonalityGroup.GetById(id);
                if (pers == null) throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Invalid personality group");
            }
            _repository.Answer.Create(new TestAnswers
            {
                AnswerContent = info.AnswerContent,
                IsDeleted = false,
                OrderIndex = info.OrderIndex,
                QuestionId = question_id
            });
            await _repository.SaveAsync();

            foreach (var id in info.PersonalityGroupId)
            {
                var newAnswer = await _repository.Answer.GetCreatedAnswer(info, question_id);
                _repository.AnswerPGroup.Create(new AnswersPGroups { AnswerId = newAnswer.AnswerId, PGroupId = id, Point = info.Point });
            }
                await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Save result from student
        /// <summary>
        /// Role: student (Saving list result)
        /// </summary>
        /// <param name="test_id">Id of test have answer</param>
        /// <param name="answersId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("result/{test_id}")]
        public async Task<IActionResult> AddResult(int test_id, SaveResult answersId)
        {
            var userId = _userAccessor.GetAccountId();
            var questionsId = await _repository.Question.GetForSavingAnswer(test_id);

            foreach(var ansId in answersId.ListAnswerId)
            {
                var answer = await _repository.Answer.GetAnswerById(ansId);
                if (!questionsId.Contains(answer.QuestionId))
                {
                    throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, string.Format($"Answer id: {0} isn't belong to this test", ansId));
                }
                else
                {
                    await _repository.TestResult.CreateResult(new TestResults
                    {
                        AnswerId = ansId,
                        CreatedDate = DateTime.UtcNow,
                        IsLast = true,
                        QuestionId = answer.QuestionId,
                        TestId = test_id,
                        UserId = userId
                    });
                }
            }
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        #region Get answer by question id
        /// <summary>
        /// Role: All (Admin to update answer, student for get answer of test)
        /// </summary>
        /// <param name="question_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("question/{question_id}")]
        public async Task<IActionResult> GetByQuestionId(int question_id)
        {
            var result = await _repository.Answer.GetByQuestionId(question_id);
            return Ok(result);
        }
        #endregion

        /// <summary>
        /// Role: Admin (get answer detail)
        /// </summary>
        /// <param name="answer_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{answer_id}/detail")]
        public async Task<IActionResult> GetAnswerDetail(int answer_id)
        {
            var result = await _repository.AnswerPGroup.GetAnswerDetail(answer_id);

            if(result == null)
            {
                var answer = await _repository.Answer.GetAnswerById(answer_id);
                result = new AnswerDetail { Answer = new AnswerOfQuestion { AnswerId = answer.AnswerId, AnswerContent = answer.AnswerContent, OrderIndex = answer.OrderIndex }, PeronalityGroups = null };
            }

            return Ok(result);
        }

        #region Update answer
        /// <summary>
        /// Role: Admin (Update answer by id)
        /// </summary>
        /// <param name="answer_id"></param>
        /// <param name="info"></param>
        /// <returns></returns>
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

            await _repository.Answer.Update(answer_id, info);
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion
    }
}
