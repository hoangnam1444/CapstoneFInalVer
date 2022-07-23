using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/colleges")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public CollegesController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Role: Admin (get all colleges)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllColleges([FromQuery] PagingParameters param)
        {
            var role = _userAccessor.GetAccountRole();
            if(role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            List<CollegesInList> result = await _repository.Colleges.GetColleges(param);

            return Ok(result);
        }

        /// <summary>
        /// Role: Admin (get college detail)
        /// </summary>
        /// <param name="colleges_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{colleges_id}/detail")]
        public async Task<IActionResult> GetCollegesDetail(int colleges_id)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            var college = await _repository.Colleges.GetDetail(colleges_id);

            college = await _repository.MajorColleges.GetMajor(college);

            college = await _repository.MajorSubjectGroupColleges.GetSumPoint(college);

            return Ok(college);
        }

        /// <summary>
        /// Role: Admin (New colleges)
        /// </summary>
        /// <param name="college"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateCollege(NewCollege college)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            _repository.Colleges.Create(new Colleges
            {
                Address = college.Address,
                CollegeName = college.CollegeName,
                ReferenceLink = college.ReferenceLink,
                ImagePath = college.ImagePath,
                IsDeleted = false
            });
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: Admin (add new major of colleges)
        /// </summary>
        /// <param name="majorId"></param>
        /// <param name="college_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{college_id}/major/{majorId}")]
        public async Task<IActionResult> AddMajorToColleges(int majorId, int college_id)
        {
            _repository.MajorColleges.Create(new CollegeRefMajor { CollegeId = college_id, MajorId = majorId, IsDeleted = false });
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: Admin (add new major of colleges)
        /// </summary>
        /// <param name="majorId"></param>
        /// <param name="college_id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{college_id}/major/{majorId}")]
        public async Task<IActionResult> RemoveMajorOfColleges(int majorId, int college_id)
        {
            _repository.MajorColleges.Delete(new CollegeRefMajor { CollegeId = college_id, MajorId = majorId, IsDeleted = false });
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: Admin (add point of subject)
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Point")]
        public async Task<IActionResult> AddNewPoint(PointCollege point)
        {
            _repository.MajorSubjectGroupColleges.Create(new CollegesSubjectGroup
            {
                MajorId = point.MajorId,
                SubjectGroupId = point.SubjectGroupId,
                Sum = point.Sum,
                CollegesId = point.CollegesId,
            });
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
    }
}
