using Contracts.Repositories;
using Entities.DTOs;
using Entities.RequestFeature;
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
            result = await _repository.SubjectGroupSubject.GetSubjectOfGroup(result);
            return Ok(result);
        }

        /// <summary>
        /// Role: Student (Get lession by majorsId)
        /// </summary>
        /// <param name="majors"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lession")]
        public async Task<IActionResult> GetLession(GetLessionByMajors majors)
        {
            var result = await _repository.LessionMajor.GetLessionbyListMajor(majors.MajorsId);

            return Ok(result);
        }

        /// <summary>
        /// Role: Student (get colleges by major id return in result)
        /// </summary>
        /// <param name="major_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{major_id}/Colleges")]
        public async Task<IActionResult> GetColleges(int major_id)
        {
            var result = await _repository.MajorColleges.GetSuggesionColleges(major_id);

            result = await _repository.MajorColleges.GetMajor(result);

            result = await _repository.MajorSubjectGroupColleges.GetSumPoint(result);

            return Ok(result);
        }

        /// <summary>
        /// Role: Admin (Get list major)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListMajors([FromQuery] PagingParameters param)
        {
            var majors = await _repository.Major.GetAll(param);

            return Ok(majors);
        }

        /// <summary>
        /// Role: Admin (get major for filter)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("ForFiltering")]
        public async Task<IActionResult> GetMajorForFilter()
        {
            List<MajorForFilter> majors = await _repository.Major.GetAll();

            return Ok(majors);
        }
    }
}
