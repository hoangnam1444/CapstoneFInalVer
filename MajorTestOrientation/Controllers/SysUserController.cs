using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
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

        public SysUserController(IRepositoryManager repository, IFirebaseService firebase, IJwtServices jwtServices)
        {
            _repository = repository;
            _firebase = firebase;
            _jwtServices = jwtServices;
        }


        #region Login by gg mail
        /// <summary>
        /// Login by google mail (Role: ALL)
        /// </summary>
        /// <param name="firebaseToken">Token get from firebase</param>
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
                    IsLocked = false,
                    IsDeleted = false
                };
                _repository.SysUser.Create(new_account);
                await _repository.SaveAsync();
                //get account return
                account = await _repository.SysUser.GetAccountByGmail(firebaseProfile.Email);
            }
            account.Token = _jwtServices.CreateToken(account.RoleId, account.Id);
            return Ok(account);
        }
        #endregion

    }
}
