using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DataTransferObject;
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

            var result = await _repository.Colleges.GetColleges(param);

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
        /// Role: Admin (create new colleges)
        /// </summary>
        /// <param name="colleges">List colleges get from excel file</param>
        /// <returns></returns>
        [HttpPost]
        [Route("import")]
        public async Task<IActionResult> CreateCollege(List<NewCollege> colleges)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permission");
            }

            foreach (var college in colleges)
            {
                _repository.Colleges.Create(new Colleges
                {
                    Address = college.Address,
                    CollegeName = college.CollegeName,
                    ReferenceLink = college.ReferenceLink,
                    ImagePath = college.ImagePath,
                    IsDeleted = false
                });
            }
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
        [HttpPut]
        [Route("Point")]
        public async Task<IActionResult> AddNewPoint(PointCollege point)
        {
            CollegesSubjectGroup info = await _repository.MajorSubjectGroupColleges.GetPoint(point);

            var updateInfo = new CollegesSubjectGroup
            {
                MajorId = point.MajorId,
                SubjectGroupId = point.SubjectGroupId,
                Sum = point.Sum,
                CollegesId = point.CollegesId
            };

            if (info == null)
            {
                _repository.MajorSubjectGroupColleges.Create(updateInfo);
            } else
            {
                _repository.MajorSubjectGroupColleges.Update(updateInfo);
            }
            await _repository.SaveAsync();
            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: Admin (Update colleges info)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateColleges(UpdateCollege info)
        {
            _repository.Colleges.Update(new Colleges
            {
                CollegeId = info.CollegeId,
                Address = info.Address,
                CollegeName = info.CollegeName,
                ImagePath = info.ImagePath,
                ReferenceLink = info.ReferenceLink,
                IsDeleted = false
            });
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: student (get all colleges)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Dashboard")]
        public async Task<IActionResult> GetColleges([FromQuery] PagingParameters param)
        {
            Pagination<CollegesReturn> colleges = await _repository.Colleges.GetAll(param);

            var result = new DashboardColleges
            {
                ViewPointLink = "https://vietnamnet.vn/giao-duc/diem-thi/tra-cuu-diem-chuan-cd-dh-2022",
                College = colleges
            };

            return Ok(result);
        }

        /// <summary>
        /// Role: Admin (statistic selected colleges)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("statistic")]
        public async Task<IActionResult> Statistic([FromQuery]PagingParameters param)
        {
            Pagination<CollegesStatistic> colleges = await _repository.UserCollege.Statistic(param);

            colleges.Data = await _repository.Colleges.GetName(colleges.Data);

            return Ok(colleges);
        }
    }
}

