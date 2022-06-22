using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.EntityFrameworkCore;
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

        public async Task<SysUser> GetToUpdateGrade(int id)
        {
            return await FindByCondition(x => x.UserId == id, false).FirstOrDefaultAsync();
        }
    }
}
