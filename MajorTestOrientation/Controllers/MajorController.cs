using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/majors")]
    [ApiController]
    public class MajorController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public MajorController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
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
            await _repository.SysUser.UpdateActiveTime(_userAccessor.GetAccountId());

            await _repository.SaveAsync();
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
            await _repository.SysUser.UpdateActiveTime(_userAccessor.GetAccountId());

            await _repository.SaveAsync();
            return Ok(result);
        }

        /// <summary>
        /// Role: Student (Get lession by majorsId)
        /// </summary>
        /// <param name="majors"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("lesson")]
        public async Task<IActionResult> GetLession(GetLessionByMajors majors)
        {
            var result = await _repository.LessionMajor.GetLessionbyListMajor(majors.MajorsId);
            await _repository.SysUser.UpdateActiveTime(_userAccessor.GetAccountId());

            await _repository.SaveAsync();
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

            result = await _repository.UserCollege.GetSelectedUser(result, _userAccessor.GetAccountId());

            result = await _repository.MajorColleges.GetMajor(result);

            result = await _repository.MajorSubjectGroupColleges.GetSumPoint(result);
            await _repository.SysUser.UpdateActiveTime(_userAccessor.GetAccountId());

            await _repository.SaveAsync();
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
            var majors = await _repository.Major.GetAll();

            return Ok(majors);
        }

        /// <summary>
        /// Role: Admin (Statistic major selected by user)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("statistic")]
        public async Task<IActionResult> StatisticMajor([FromQuery] PagingParameters param)
        {
            var majors = await _repository.MajorUser.Statistic(param);

            majors.Data = await _repository.Major.GetMajorName(majors.Data);

            return Ok(majors);
        }

        /// <summary>
        /// Role: Admin (New major)
        /// </summary>
        /// <param name="major"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateMajor(NewMajor major)
        {
            var newMajor = new Entities.Models.Majors
            {
                MajorName = major.Name
            };
            _repository.Major.Create(newMajor);
            await _repository.SaveAsync();
            foreach(var subjectId in major.SubjectGroupIds)
            {
                _repository.SubjectGroupMajor.Create(new MajorSubjectGroup
                {
                    MajorId = newMajor.MajorId,
                    SubjectGroupId = subjectId
                });
            }
            await _repository.SaveAsync();

            return Ok("Save change success");
        }

    }
}
