using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISysUserRepository
    {
        Task<AfterLoginInfo> GetAccountByGmail(string email);
        Task<SysUser> GetAccountByUnPw(AdminLogin info);
        Task<SysUser> GetToUpdateGrade(int id);
        Task<List<SysUserReturn>> GetAllSysUser();
        Task<UserDetail> GetUserDetail(int user_id);
        Task AvtivateAccount(int userId);
        void Create(SysUser sysUser);
        void Update(SysUser sysUser);
        Task<SysUser> GetById(int userId);
        Task<Profile> GetProfile(int user_id);
        Task<Pagination<Connector>> GetConnector(int status, PagingParameters param);
        Task<SysUser> GetConnector(int v);
        Task<List<ChatBoxAccount>> GetAvailableConnector(List<ChatBoxAccount> accounts);
        Task<SysUser> GetCreatedAccount(string email, string userName);
        Task<List<SysUser>> GetLockAccount();
        Task UpdateActiveTime(int userId);
        Task<ChatInfo> GetChatInfo(int user_id);
    }
}
