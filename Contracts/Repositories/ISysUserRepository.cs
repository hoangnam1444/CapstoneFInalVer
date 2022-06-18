using Entities.DTOs;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISysUserRepository
    {
        Task<AfterLoginInfo> GetAccountByGmail(string email);
        void Create(SysUser sysUser);
    }
}
