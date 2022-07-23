using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using static Entities.DTOs.ErrorDetails;

namespace MajorTestOrientation.Controllers
{
    [Route("api/sys_users")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IFirebaseService _firebase;
        private readonly IJwtServices _jwtServices;
        private readonly IUserAccessor _userAccessor;
        private readonly IHasingServices _hasing;

        public SysUserController(IRepositoryManager repository, IFirebaseService firebase, IJwtServices jwtServices, IUserAccessor userAccessor,
            IHasingServices hasing)
        {
            _repository = repository;
            _firebase = firebase;
            _jwtServices = jwtServices;
            _userAccessor = userAccessor;
            _hasing = hasing;
        }


        #region Login by gg mail
        /// <summary>
        /// Role: Student (Login by gmail)
        /// </summary>
        /// <param name="firebaseToken"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("login_by_email")]
        public async Task<IActionResult> LoginByEmail(string firebaseToken)
        {
            //init firebase
            _firebase.InitFirebase();
            //get email
            var firebaseProfile = await _firebase.GetInforFromToken(firebaseToken);
            if (firebaseProfile.Email.Contains("Get email from token error: "))
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, firebaseProfile.Email);
            }

            var account = await _repository.SysUser.GetAccountByGmail(firebaseProfile.Email);
            if (account == null)
            {
                //new account
                var new_account = new SysUser
                {
                    UserName = firebaseProfile.UserName,
                    Email = firebaseProfile.Email,
                    ImagePath = firebaseProfile.Image,
                    RoleId = 1,
                    CreatedDate = System.DateTime.UtcNow,
                    UpdatedDate = System.DateTime.UtcNow,
                    IsLocked = true,
                    IsDeleted = false,
                    FullName = firebaseProfile.UserName,
                };
                _repository.SysUser.Create(new_account);
                await _repository.SaveAsync();
                //get account return
                account = await _repository.SysUser.GetAccountByGmail(firebaseProfile.Email);
            }
            account.Token = _jwtServices.CreateToken(account.RoleId, account.Id);

            if (!account.IsActive.Value)
            {
                var newCode = await _repository.SecurityCode.Create(account.Id);
                await _repository.SaveAsync();
                _userAccessor.SendEmail(account.Fullname, firebaseProfile.Email, newCode.Code);
            }

            return Ok(account);
        }
        #endregion

        #region Login by username and password
        /// <summary>
        /// Role: Admin (Use for admin login)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("admin_login")]
        public async Task<IActionResult> LoginByUnPw(AdminLogin info)
        {
            info.Password = _hasing.EncriptSHA256(info.Password);
            var account = await _repository.SysUser.GetAccountByUnPw(info);
            if (account == null)
            {
                throw new ErrorDetails(HttpStatusCode.Unauthorized);
            }
            var result = new AdminInfo
            {
                Email = account.Email,
                FullName = account.FullName,
                Id = account.UserId,
                Image = account.ImagePath,
                Token = _jwtServices.CreateToken(account.RoleId, account.UserId)
            };

            return Ok(result);
        }
        #endregion

        #region Activate account
        /// <summary>
        /// Role: Student (Activate account by security code)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("activate")]
        public async Task<IActionResult> ActiveAccount(ActivateAccount info)
        {
            var userId = _userAccessor.GetAccountId();
            var isActivated = await _repository.SecurityCode.ActivatedCode(info.Otp, userId);
            if (!isActivated)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid security code");
            }
            else
            {
                await _repository.SysUser.AvtivateAccount(userId);
                await _repository.SaveAsync();
            }
            return Ok("Save changes success");
        }
        #endregion

        #region Update grade and GPA
        /// <summary>
        /// Role: Student (Update gpa and grade after first login)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("grade_and_gpa")]
        public async Task<IActionResult> UpdateGradeAndGPA(UpdateGradeAndGpa info)
        {
            var grade = info.Grade;
            if (grade > 12 && grade < 10) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid grade");

            var accountId = _userAccessor.GetAccountId();
            var c_account = await _repository.SysUser.GetToUpdateGrade(accountId);
            c_account.Grade = grade;
            _repository.SysUser.Update(c_account);

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

        /// <summary>
        /// Role: Student (save user select subject group)
        /// </summary>
        /// <param name="group_id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("select_subject_group/{group_id}")]
        public async Task<IActionResult> SelectSubjectGroup(int group_id)
        {
            _repository.UserSubjectGroup.Create(new UserSubjectGroup
            {
                SubjectGroupId = group_id,
                UserId = _userAccessor.GetAccountId()
            });
            try
            {
                await _repository.SaveAsync();
            }
            catch
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Subject group " + group_id + " already selected", StatusCode = 415 });
            }
            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: student (get profile for update)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var user_id = _userAccessor.GetAccountId();

            var result = await _repository.SysUser.GetProfile(user_id);

            return Ok(result);
        }

        /// <summary>
        /// Role: Student (update their profile)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateProfileInfo info)
        {
            var userId = _userAccessor.GetAccountId();

            var account = await _repository.SysUser.GetById(userId);

            if(account != null)
            {
                if(info.FullName != string.Empty)
                {
                    account.FullName = info.FullName;
                }
                if(info.BirthDay != null)
                {
                    account.BirthDay = info.BirthDay;
                }
                if (info.Gender != null)
                {
                    account.Gender = info.Gender;
                }
                if (info.ImagePath != null)
                {
                    account.ImagePath = info.ImagePath;
                }
                if (info.PhoneNumber != null)
                {
                    account.PhoneNumber = info.PhoneNumber;
                }
                _repository.SysUser.Update(account);
                await _repository.SaveAsync();
            }
            return Ok("Save change success");
        }

        /// <summary>
        /// Role: Student (add point for subject)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("subject")]
        public async Task<IActionResult> UpdateSubjectUser(SubjectSelection info)
        {
            var savedPointSubjects = new List<int>();

            foreach (var item in info.ListSubject)
            {
                _repository.UserSubject.Create(new UserSubject
                {
                    Point = item.Point,
                    SubjectId = item.SubjectId,
                    UserId = _userAccessor.GetAccountId()
                });
                try
                {
                    await _repository.SaveAsync();
                }catch
                {
                    savedPointSubjects.Add(item.SubjectId);
                }
            }

            if(savedPointSubjects.Count == info.ListSubject.Count)
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "All subject has its point", StatusCode = 416 });
            }
            else if (savedPointSubjects.Count > 0)
            {
                var subjectsName = await _repository.Subject.GetName(savedPointSubjects);
                return Ok("Subject: " + subjectsName + " already saved. Save success all remaining subject");
            }

            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: student (save major of student)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("major")]
        public async Task<IActionResult> SaveMajor(AddMajor info)
        {
            try
            {
                _repository.MajorUser.Create(new UserMajor { MajorId = info.MajorId, UserId = _userAccessor.GetAccountId() });
                await _repository.SaveAsync();
            } catch
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Major "+info.MajorId+" already selected", StatusCode = 414 });
            }

            return Ok("Save changes success");
        }

        #region Get all sys user
        /// <summary>
        /// Role: Admin (get all sys_user)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permission");

            var result = await _repository.SysUser.GetAllSysUser();

            return Ok(result);
        }
        #endregion

        #region Get user detail
        /// <summary>
        /// Role: Admin (Get user detail)
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{user_id}/detail")]
        public async Task<IActionResult> GetUserDetail(int user_id)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permission");

            var result = await _repository.SysUser.GetUserDetail(user_id);

            return Ok(result);
        }
        #endregion

        #region Disale/Enable user
        /// <summary>
        /// Role: Admin (Disable/Enable user)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> DisableEnableUser(DisEnUser info)
        {
            var role = _userAccessor.GetAccountRole();
            if (role != 2)
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permission");
            var user = await _repository.SysUser.GetById(info.UserId);
            if (user == null) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid user id");

            user.IsLocked = info.EnbDisable;
            _repository.SysUser.Update(user);
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
        #endregion

        /// <summary>
        /// Role: Student (Get suggesion colleges)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("colleges")]
        public async Task<IActionResult> GetCollegesOfStudent()
        {
            var userId = _userAccessor.GetAccountId();

            var major = await _repository.MajorUser.GetMajorOfUser(userId);

            if(major.Count == 0) 
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Student don't have major", StatusCode = 410});
            }

            var subjectGroup = await _repository.UserSubjectGroup.GetSavedSubjectGroup(userId);
            var subjectGroupOfMajor = await _repository.SubjectGroupMajor.GetByMajor(major);
            var subjectGroupNeed = subjectGroup.Where(x => subjectGroupOfMajor.Contains(x.SubjectGroupId)).ToList();
            if(subjectGroupNeed.Count == 0)
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Student don't have subject group for major selected", StatusCode = 411 });
            }

            bool? hasPoint = true;
            foreach(var group in subjectGroupNeed)
            {
                var subjects = await _repository.SubjectGroupSubject.GetSubjects(group.SubjectGroupId);
                var HasEnoughPoint = await _repository.UserSubject.GetSavedSubject(userId, subjects);
                if (HasEnoughPoint == null || HasEnoughPoint == false)
                    hasPoint = HasEnoughPoint;
                if(HasEnoughPoint == true)
                {
                    hasPoint = true;
                    break;
                }
            }
            if (hasPoint == null)
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Student don't have subject point of select subject group", StatusCode = 412 });
            }
            if (!hasPoint.Value)
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Student don't have subject enough point of select subject group", StatusCode = 413 });
            }

            var datas = new List<GetCollegesData>();
            foreach(var group in subjectGroupNeed)
            {
                var subjects = await _repository.SubjectGroupSubject.GetSubjects(group.SubjectGroupId);
                var item = await _repository.UserSubject.GetSumOfSubjectGroup(subjects, userId);
                item.SubjectGroupId = group.SubjectGroupId;
                datas.Add(item);
            }

            var finalData = new List<AttempData>();
            List<MajorSubjectGroup> majorSubjectGroup = await _repository.SubjectGroupMajor.GetByMajorIds(major);
            foreach(var aMajor in majorSubjectGroup)
            {
                var item = new AttempData
                {
                    MajorId = aMajor.MajorId,
                    Data = datas.Where(x => x.SubjectGroupId == aMajor.SubjectGroupId).FirstOrDefault()
                };
                finalData.Add(item);
            }

            var result = await _repository.MajorColleges.GetSuggesionColleges(finalData);

            result = await _repository.MajorColleges.GetMajor(result);

            result = await _repository.MajorSubjectGroupColleges.GetSumPoint(result, finalData);

            return Ok(result);
        }

        /// <summary>
        /// Role: Student (Get lession detail)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("lession")]
        public async Task<IActionResult> GetLession()
        {
            var user_id = _userAccessor.GetAccountId();
            var selectedMajor = await _repository.MajorUser.GetMajorOfUser(user_id);

            var majorsId = selectedMajor.GroupBy(x => x.MajorId).Select(x => x.Key).ToList();

            var lession = await _repository.LessionMajor.GetLessionbyListMajor(majorsId);

            return Ok(lession);
        }
    }
}
