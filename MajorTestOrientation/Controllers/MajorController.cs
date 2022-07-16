using Contracts.Repositories;
using Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/majors")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IRepositoryManager _repository;

        public MajorController(IRepositoryManager repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Role: Student (get major by group_id return in test result)
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("personality_group/{group_id}")]
        public async Task<IActionResult> GetByPGroupId(int group_id)
        {
            var result = await _repository.MajorPgroup.GetByGroupId(group_id);

            if (result == null) throw new ErrorDetails(System.Net.HttpStatusCode.NotFound, "No major matches with this group");

            return Ok(result);
        }

        /// <summary>
        /// Role: Student (Get subject group after have result major)
        /// </summary>
        /// <param name="major_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{major_id}/subject_group")]
        public async Task<IActionResult> GetSubjectGroup(int major_id)
        {
            var result = await _repository.SubjectGroupMajor.GetByMajor(major_id);

            return Ok(result);
        }
    }
}
