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

        #region Get MBTI text
        /// <summary>
        /// Get mbti question
        /// </summary>
        /// <param name="index">index of question</param>
        /// <returns></returns>
        [HttpGet]
        [Route("mbti/{index}")]
        public async Task<IActionResult> GetMBTIQuestion(int index)
        {
            var result = new MbtiQuestion();
            var question = await _repository.Question.GetMBTIQuestion(index);
            result.QuestionId = question.QuestionId;
            result.Content = question.QuestionContent;
            result.Index = question.OrderIndex;
            var answer = await _repository.Answer.GetMbtiAnswer(question.QuestionId);
            result.Answer = answer;
            return Ok(result);
        }
        #endregion
    }
}
