using Entities.DTOs;
using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISysUserRepository
    {
        Task<AfterLoginInfo> GetAccountByGmail(string email);
        Task<SysUser> GetToUpdateGrade(int id);
        void Create(SysUser sysUser);
        void Update(SysUser sysUser);
    }
}
