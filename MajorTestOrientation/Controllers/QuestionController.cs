using Contracts.Repositories;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/questions")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public QuestionController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        #region Get MBTI test questionIdList
        /// <summary>
        /// Get all id of mbti question
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
        /// Get mbti question
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
        /// Get all holland question
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("holland")]
        public async Task<IActionResult> GetHollandTest()
        {
            var result = await _repository.Question.GetHollandTest();
            return Ok(result);
        }
        #endregion
    }
}
