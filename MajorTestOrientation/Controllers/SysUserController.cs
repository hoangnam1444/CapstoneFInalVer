using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

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
        /// Login by gmail
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

            if (!account.HasGrade)
            {
                var newCode = _repository.SecurityCode.Create(account.Id);
                await _repository.SaveAsync();
                //await _userAccessor.SendEmail(account.Fullname, firebaseProfile.Email, newCode.Code);
            }

            return Ok(account);
        }
        #endregion

        #region Login by username and password
        /// <summary>
        /// Use for admin login
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
        /// Avtivate account by security code
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("activate")]
        public async Task<IActionResult> ActiveAccount(ActivateAccount info)
        {
            var userId = _userAccessor.GetAccountId();
            var isActivated = await _repository.SecurityCode.ActivatedCode(info.sCode, userId);
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
        /// Update gpa and grade after first login
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("grade_and_gpa")]
        public async Task<IActionResult> UpdateGradeAndGPA(UpdateGradeAndGpa info)
        {
            var grade = info.Grade;
            if (grade > 12 && grade < 10) throw new ErrorDetails(HttpStatusCode.BadRequest, "Invalid grade");
            if (info.GPA10 == null)
            {
                throw new ErrorDetails(HttpStatusCode.BadRequest, "GPA of grade 10 can't null");
            }
            else if (info.GPA11 == null)
            {
                if (grade != 10)
                {
                    throw new ErrorDetails(HttpStatusCode.BadRequest, "GPA of grade 11 can't null");
                }
            }
            else if (info.GPA12 == null)
            {
                if (grade == 12)
                {
                    throw new ErrorDetails(HttpStatusCode.BadRequest, "GPA of grade 12 can't null");
                }
            }

            var accountId = _userAccessor.GetAccountId();
            var c_account = await _repository.SysUser.GetToUpdateGrade(accountId);
            c_account.Grade = grade;
            c_account.Gpa10 = info.GPA10;
            c_account.Gpa11 = info.GPA11;
            c_account.Gpa12 = info.GPA12;
            _repository.SysUser.Update(c_account);

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }
        #endregion

    }
}
