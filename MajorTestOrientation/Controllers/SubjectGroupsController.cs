using Contracts.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/subject_groups")]
    [ApiController]
    public class SubjectGroupsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public SubjectGroupsController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Role: Student (Get subject by subject group id)
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{group_id}/subject")]
        public async Task<IActionResult> GetSubject(int group_id)
        {
            var result = await _repository.SubjectGroupSubject.GetSubject(group_id);

            return Ok(result);
        }
    }
}
