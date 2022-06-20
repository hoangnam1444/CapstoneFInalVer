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
        private ISecurityCodeRepository _sCode;
        private IQuestionRepository _question;
        private IAnswerRepository _answer;
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

        public ISecurityCodeRepository SecurityCode
        {
            get
            {
                if(_sCode == null)
                {
                    _sCode = new SecurityCodeRepository(_context);
                }
                return _sCode;
            }
        }

        public IQuestionRepository Question
        {
            get
            {
                if(_question == null)
                {
                    _question = new QuestionRepository(_context);
                }
                return _question;
            }
        }

        public IAnswerRepository Answer
        {
            get
            {
                if(_answer == null)
                {
                    _answer = new AnswerRepository(_context);
                }
                return _answer;
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
