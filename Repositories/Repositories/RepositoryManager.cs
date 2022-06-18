using Contracts.Repositories;
using Entities;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class RepositoryManager : IRepositoryManager
    {
        public RepositoryManager(DataContext context)
        {
            _context = context;
        }

        private ISysUserRepository _sysUser;
        private readonly DataContext _context;

        public ISysUserRepository SysUser
        {
            get
            {
                if(_sysUser == null)
                {
                    _sysUser = new SysUserRepository(_context);
                }
                return _sysUser;
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
