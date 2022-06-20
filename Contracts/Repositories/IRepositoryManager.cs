using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ISysUserRepository SysUser { get; }
        ISecurityCodeRepository SecurityCode { get; }
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        Task SaveAsync();
    }
}
