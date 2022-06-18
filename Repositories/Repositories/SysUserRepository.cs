using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class SysUserRepository : RepositoryBase<SysUser>, ISysUserRepository
    {
        public SysUserRepository(DataContext context) : base(context)
        {
        }

        public async Task<AfterLoginInfo> GetAccountByGmail(string email)
        {
            var account = await FindByCondition(x => x.Email.Equals(email) && x.IsDeleted == false && x.IsLocked == false, false).FirstOrDefaultAsync();
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
    }
}
