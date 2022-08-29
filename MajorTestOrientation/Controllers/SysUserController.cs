using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                    LastLoginDate = DateTime.UtcNow
                };
                _repository.SysUser.Create(new_account);
                await _repository.SaveAsync();
                //get account return
                account = await _repository.SysUser.GetAccountByGmail(firebaseProfile.Email);
            }
            else
            {
                var savedAccount = await _repository.SysUser.GetById(account.Id);
                savedAccount.LastLoginDate = DateTime.UtcNow;
                _repository.SysUser.Update(savedAccount);
                await _repository.SaveAsync();
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

            if (account != null)
            {
                if (info.FullName != string.Empty)
                {
                    account.FullName = info.FullName;
                }
                if (info.BirthDay != null)
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
        /// Role: System handler (lock unused account, schedule for the next day)
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("disable_unsued_users")]
        public async Task<IActionResult> DisableUnuseAccount()
        {
            var disabledAccount = 0;
            List<SysUser> lockAccount = await _repository.SysUser.GetLockAccount();
            foreach(var account in lockAccount)
            {
                if(account.LastLoginDate < DateTime.UtcNow.AddDays(-30))
                {
                    account.IsLocked = true;
                    _repository.SysUser.Update(account);
                    disabledAccount++;
                }
            }
            if(disabledAccount > 0)
            {
                await _repository.SaveAsync();
            }
            
            SavedSchedule scheduledEvt = await _repository.SaveSchedule.GetByDay(DateTime.UtcNow.AddDays(1).Day);
            if(scheduledEvt == null)
            {
                var timeShedule = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.AddDays(1).Day, 7, 0, 0, DateTimeKind.Local);
                var jobId = BackgroundJob.Schedule(() => DisableUnuseAccount(), timeShedule);
                _repository.SaveSchedule.Create(new SavedSchedule { Day = timeShedule.Day, ScheduleId = jobId });
                await _repository.SaveAsync();
            }
            return Ok("Save changes success");
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
                }
                catch
                {
                    savedPointSubjects.Add(item.SubjectId);
                }
            }

            if (savedPointSubjects.Count == info.ListSubject.Count)
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
            }
            catch
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Major " + info.MajorId + " already selected", StatusCode = 414 });
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
        /// Role: Admin, User, Connector (Get connector, only ADMIN can get with status ALL and UNAVAILABLE)
        /// </summary>
        /// <param name="status">2: All, 1: Available, 0: Unavailable</param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("connector/{status}")]
        public async Task<IActionResult> GetConnectorByStatus(int status, [FromQuery] PagingParameters param)
        {
            if (status != 1 && _userAccessor.GetAccountRole() != 2)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permisson");
            }
            //IsAvalable of connector = !IsDeleted in SysUser
            Pagination<Connector> connectors = await _repository.SysUser.GetConnector(status, param);
            return Ok(connectors);
        }

        /// <summary>
        /// Role: Connector (Update their status)
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("status")]
        public async Task<IActionResult> UpdateStatusConnector()
        {
            if (_userAccessor.GetAccountRole() != 3)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permisson");
            }

            SysUser account = await _repository.SysUser.GetConnector(_userAccessor.GetAccountId());
            if (account.IsDeleted.Value)
            {
                account.IsDeleted = false;
            }
            else
            {
                account.IsDeleted = true;
            }
            _repository.SysUser.Update(account);
            await _repository.SaveAsync();

            return Ok(new { UpdatedStatus = account.IsDeleted.Value ? "Unavailable" : "Available" });
        }

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

            if (major.Count == 0)
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Student don't have major", StatusCode = 410 });
            }

            var subjectGroup = await _repository.UserSubjectGroup.GetSavedSubjectGroup(userId);
            var subjectGroupOfMajor = await _repository.SubjectGroupMajor.GetByMajor(major);
            var subjectGroupNeed = subjectGroup.Where(x => subjectGroupOfMajor.Contains(x.SubjectGroupId)).ToList();
            if (subjectGroupNeed.Count == 0)
            {
                throw new ErrorDetails(HttpStatusCode.OK, new GetCollegesHandle { Message = "Student don't have subject group for major selected", StatusCode = 411 });
            }

            bool? hasPoint = true;
            foreach (var group in subjectGroupNeed)
            {
                var subjects = await _repository.SubjectGroupSubject.GetSubjects(group.SubjectGroupId);
                var HasEnoughPoint = await _repository.UserSubject.GetSavedSubject(userId, subjects);
                if (HasEnoughPoint == null || HasEnoughPoint == false)
                    hasPoint = HasEnoughPoint;
                if (HasEnoughPoint == true)
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
            foreach (var group in subjectGroupNeed)
            {
                var subjects = await _repository.SubjectGroupSubject.GetSubjects(group.SubjectGroupId);
                var item = await _repository.UserSubject.GetSumOfSubjectGroup(subjects, userId);
                item.SubjectGroupId = group.SubjectGroupId;
                datas.Add(item);
            }

            var finalData = new List<AttempData>();
            var majorSubjectGroup = await _repository.SubjectGroupMajor.GetByMajorIds(major);
            foreach (var aMajor in majorSubjectGroup)
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
        [Route("lesson")]
        public async Task<IActionResult> GetLession()
        {
            var user_id = _userAccessor.GetAccountId();

            var lesson = await _repository.LessionMajor.GetAll();

            return Ok(lesson);
        }

        /// <summary>
        /// Role: Student (Get lesson by major selected)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("lesson_by_major")]
        public async Task<IActionResult> GetLessionByMajor()
        {
            var user_id = _userAccessor.GetAccountId();
            var selectedMajor = await _repository.MajorUser.GetMajorOfUser(user_id);

            var majorsId = selectedMajor.GroupBy(x => x.MajorId).Select(x => x.Key).ToList();

            var lesson = await _repository.LessionMajor.GetLessionbyListMajor(majorsId);

            return Ok(lesson);
        }

        /// <summary>
        /// Role: Student (save colleges)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("colleges")]
        public async Task<IActionResult> SaveColleges(SaveColleges info)
        {
            var user_id = _userAccessor.GetAccountId();

            var savedColleges = await _repository.UserCollege.SelectedColleges(info.CollegesId, user_id);
            if (savedColleges)
            {
                _repository.UserCollege.Delete(new UserColleges { CollegeId = info.CollegesId, UserId = user_id });
            }
            else
            {
                _repository.UserCollege.Create(new UserColleges { CollegeId = info.CollegesId, UserId = user_id, IsConnector = false });
            }
            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: Admin (Create connector, Colleges id get from API get all colleges)
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Connector")]
        public async Task<IActionResult> CreateConnector(NewConnectorInfo info)
        {
            if (_userAccessor.GetAccountRole() != 2)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permission");
            }

            var hasedPw = _hasing.EncriptSHA256(info.Password);

            var newUser = await _repository.SysUser.GetCreatedAccount(info.Email, info.UserName);

            if (newUser == null)
            {
                newUser = new SysUser
                {
                    FullName = info.FullName,
                    UserName = info.UserName,
                    PhoneNumber = info.PhoneNumber,
                    Password = hasedPw,
                    Gender = info.Gender,
                    AdminIdUpdate = _userAccessor.GetAccountId(),
                    CreatedDate = DateTime.UtcNow,
                    Email = info.Email,
                    IsDeleted = false,
                    IsLocked = false,
                    RoleId = 3
                };

                _repository.SysUser.Create(newUser);
            }
            else
            {
                newUser.FullName = info.FullName;
                newUser.UserName = info.UserName;
                newUser.PhoneNumber = info.PhoneNumber;
                newUser.Password = hasedPw;
                newUser.Gender = info.Gender;
                newUser.AdminIdUpdate = _userAccessor.GetAccountId();
                newUser.CreatedDate = DateTime.UtcNow;
                newUser.Email = info.Email;
                newUser.IsDeleted = false;
                newUser.IsLocked = false;
                newUser.RoleId = 3;

                _repository.SysUser.Update(newUser);
            }
            await _repository.SaveAsync();

            try
            {
                _repository.UserCollege.Create(new UserColleges
                {
                    CollegeId = info.CollegesId,
                    UserId = newUser.UserId,
                    IsConnector = true
                });
            }
            catch (Exception)
            {
                _repository.UserCollege.Update(new UserColleges
                {
                    CollegeId = info.CollegesId,
                    UserId = newUser.UserId,
                    IsConnector = true
                });
            }

            await _repository.SaveAsync();

            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: student (get wishlist colleges)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("colleges/wishlist")]
        public async Task<IActionResult> GetWishlistColleges()
        {
            var result = await _repository.UserCollege.GetWishlist(_userAccessor.GetAccountId());

            result = await _repository.MajorColleges.GetMajor(result);

            result = await _repository.MajorSubjectGroupColleges.GetSumPoint(result);

            return Ok(result);
        }

        /// <summary>
        /// Role: Connector (Get all chat created with student)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("ChatRoom")]
        public async Task<IActionResult> GetChatWithStudent([FromQuery]PagingParameters param)
        {
            var connectorId = _userAccessor.GetAccountId();

            var result = await _repository.ChatRoom.GetChatWithStudent(connectorId, param);

            return Ok(result);
        }

        /// <summary>
        /// Role: Admin (update role of user)
        /// </summary>
        /// <param name="account_id">account will update</param>
        /// <param name="role_id">1: student, 3: connector</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{account_id}/role/{role_id}")]
        public async Task<IActionResult> UpdateRole(int account_id, int role_id)
        {
            var cRequestRole = _userAccessor.GetAccountRole();
            if(cRequestRole != 2)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Don't have permission");
            }
            if(role_id == 2)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Not accept for update user to admin");
            }
            var user = await _repository.SysUser.GetById(account_id);
            if(user.RoleId == 2)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "Not accept to update admin to another role");
            }

            user.RoleId = role_id;
            _repository.SysUser.Update(user);
            await _repository.SaveAsync();
            return Ok("Save change success");
        }

        /// <summary>
        /// Role: Student (Get room chat with connector)
        /// </summary>
        /// <param name="accountId">Connector id</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{accountId}/openchat")]
        public async Task<IActionResult> OpenChat(int accountId)
        {
            var senderId = _userAccessor.GetAccountId();
            var receiverId = accountId;
            bool isExistRoom = await _repository.ChatRoom.IsExistRoom(senderId, receiverId);

            ChatRoom chatRoom;
            if (isExistRoom)
            {
                chatRoom = await _repository.ChatRoom.GetExistChatRoom(senderId, receiverId);
            }
            else
            {
                chatRoom = new ChatRoom
                {
                    CollegeId = await _repository.UserCollege.GetConColId(receiverId),
                    ConnectorId = receiverId,
                    StudentId = senderId
                };
                _repository.ChatRoom.Create(chatRoom);

                await _repository.SaveAsync();
            }

            var connector = await _repository.SysUser.GetById(chatRoom.ConnectorId);

            var result = new RoomChatReturn
            {
                RoomId = chatRoom.Id,
                CollegeName = _repository.Colleges.GetDetail(chatRoom.CollegeId).Result.CollegeName,
                ConnectorAvatar = connector.ImagePath,
                ConnectorName = connector.UserName
            };

            return Ok(chatRoom);
        }
    }
}
