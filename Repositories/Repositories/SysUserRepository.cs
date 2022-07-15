using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SysUserRepository : RepositoryBase<SysUser>, ISysUserRepository
    {
        public SysUserRepository(DataContext context) : base(context)
        {
        }

        public async Task AvtivateAccount(int userId)
        {
            var user = await FindByCondition(x => x.UserId == userId, true).FirstOrDefaultAsync();
            user.IsLocked = false;
            Update(user);
        }

        public async Task<AfterLoginInfo> GetAccountByGmail(string email)
        {
            var account = await FindByCondition(x => x.Email.Equals(email) && x.IsDeleted == false, false).FirstOrDefaultAsync();
            if (account == null) return null;
            return new AfterLoginInfo
            {
                Avatar = account.ImagePath,
                Fullname = account.FullName,
                Id = account.UserId,
                HasGrade = account.Grade != null,
                Username = account.UserName,
                RoleId = account.RoleId
            };
        }

        public async Task<SysUser> GetAccountByUnPw(AdminLogin info)
        {
            return await FindByCondition(x => x.Email == info.Email && x.Password == info.Password, false).FirstOrDefaultAsync();
        }

        public async Task<List<SysUserReturn>> GetAllSysUser()
        {
            var users = await FindByCondition(x => x.IsDeleted == false, false).Include(x => x.Role).ToListAsync();

            var result = users.Select(x => new SysUserReturn
            {
                UserId = x.UserId,
                CreatedDate = x.CreatedDate,
                Email = x.Email,
                ImagePath = x.ImagePath,
                IsLocked = x.IsLocked,
                PhoneNumber = x.PhoneNumber,
                RoleName = x.Role.RoleName,
                UpdatedDate = x.UpdatedDate,
                UserName = x.UserName
            }).ToList();

            return result;
        }

        public async Task<SysUser> GetById(int userId)
        {
            return await FindByCondition(x => x.UserId == userId, false).FirstOrDefaultAsync();
        }

        public async Task<SysUser> GetToUpdateGrade(int id)
        {
            return await FindByCondition(x => x.UserId == id, false).FirstOrDefaultAsync();
        }

        public async Task<UserDetail> GetUserDetail(int user_id)
        {
            var user = await FindByCondition(x => x.UserId == user_id && x.IsDeleted == false, false)
                .Include(x => x.UpdateAdmin)
                .Include(x => x.Role)
                .FirstOrDefaultAsync();

            if(user != null)
            {
                return new UserDetail
                {
                    BirthDay = user.BirthDay,
                    CreatedDate = user.CreatedDate,
                    Email = user.Email,
                    FullName = user.FullName,
                    Gender = user.Gender,
                    Gpa10 = user.Gpa10,
                    Gpa11 = user.Gpa11,
                    Gpa12 = user.Gpa12,
                    Grade = user.Grade,
                    ImagePath = user.ImagePath,
                    IsLocked = user.IsLocked,
                    PhoneNumber = user.PhoneNumber,
                    RoleName = user.Role.RoleName,
                    AdminUpdate = user.UpdateAdmin != null ? new AdminUpdateInfo
                    {
                        AdminId = user.UpdateAdmin.UserId,
                        ImagePath = user.UpdateAdmin.ImagePath,
                        Name = user.UpdateAdmin.UserName
                    } : null,
                    UpdatedDate = user.UpdatedDate,
                    UserId = user.UserId,
                    UserName = user.UserName
                };
            }
            return null;
        }
    }
}
