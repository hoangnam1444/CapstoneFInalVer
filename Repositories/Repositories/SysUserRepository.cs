using Contracts.Repositories;
using Entities;
using Entities.DataTransferObject;
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
                RoleId = account.RoleId,
                IsActive = !account.IsLocked
            };
        }

        public async Task<SysUser> GetAccountByUnPw(AdminLogin info)
        {
            var account = await FindByCondition(x => x.Email == info.Email && x.Password == info.Password, false).FirstOrDefaultAsync();
            if(account == null)
            {
                account = await FindByCondition(x => x.UserName == info.Email && x.Password == info.Password, false).FirstOrDefaultAsync();
            }
            return account;
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

        public async Task<Pagination<Connector>> GetConnector(int status, PagingParameters param)
        {
            var accounts = new List<Connector>();
            var count = 0;
            if(status == 0)
            {
                accounts = await FindByCondition(x => x.IsDeleted == true, false)
                    .Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize)
                    .Select(x => new Connector
                    {
                        Id = x.UserId,
                        Fullname = x.FullName,
                        ImagePath = x.ImagePath,
                        Status = "Unavalailable"
                    }).ToListAsync();
                count = await FindByCondition(x => x.IsDeleted == true, false).CountAsync();
            }else if(status == 1)
            {
                accounts = await FindByCondition(x => x.IsDeleted == false, false)
                    .Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize)
                    .Select(x => new Connector
                    {
                        Id = x.UserId,
                        Fullname = x.FullName,
                        ImagePath = x.ImagePath,
                        Status = "Avalailable"
                    }).ToListAsync();
                count = await FindByCondition(x => x.IsDeleted == false, false).CountAsync();
            }
            else
            {
                accounts = await FindAll(false)
                    .Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize)
                    .Select(x => new Connector
                    {
                        Id = x.UserId,
                        Fullname = x.FullName,
                        ImagePath = x.ImagePath,
                        Status = x.IsDeleted.Value ? "Unavailable" : "Available"
                    }).ToListAsync();
                count = await FindAll(false).CountAsync();
            }
            return new Pagination<Connector>
            {
                Count = count,
                Data = accounts,
                PageNumber = param.PageNumber,
                PageSize = param.PageSize
            };
        }

        public async Task<SysUser> GetConnector(int v)
        {
            return await FindByCondition(x => x.UserId == v, false).FirstOrDefaultAsync();
        }

        public async Task<Profile> GetProfile(int user_id)
        {
            var result = await FindByCondition(x => x.UserId == user_id, false).Select(x => new Profile
            {
                BirthDate = x.BirthDay,
                FullName = x.FullName,
                Gender = x.Gender,
                ImagePath = x.ImagePath,
                PhoneNumber = x.PhoneNumber
            }).FirstOrDefaultAsync();

            return result;
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
