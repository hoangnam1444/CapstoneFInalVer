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
        private ITestResultRepository _result;
        private ITestDeclarationRepository _test;
        private ITestTypeRepository _type;
        private IPersonalityGroupRepository _pGroup;
        private IMajorPGroupRepository _pGroupMajor;
        private IPgroupAnswerRepository _pGroupAnswer;
        private ISubjectGroupMajorRepository _subGroup;
        private SubjectGroupSubjectRepository _subGroupSub;
        private IUserSubjectGroupRepository _userSubGroups;
        private IUserSubjectRepository _userSubs;
        private IMajorCollegesRepository _majorCol;
        private IUserMajorRepository _userMajor;
        private IMajorSubjectGroupCollegesRepository _majorSubjectGroupColleges;
        private ILessionMajorRepository _lession;
        private ISubjectRepository _subject;
        private ICollegesRepository _colleges;
        private IMajorRepository _major;
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

        public ITestResultRepository TestResult
        {
            get
            {
                if(_result == null)
                {
                    _result = new TestResultRepository(_context);
                }
                return _result;
            }
        }

        public ITestDeclarationRepository Test
        {
            get
            {
                if(_test == null)
                {
                    _test = new TestDeclarationRepository(_context);
                }
                return _test;
            }
        }

        public ITestTypeRepository TestType
        {
            get
            {
                if(_type == null)
                {
                    _type = new TestTypeRepository(_context);
                }
                return _type;
            }
        }

        public IPersonalityGroupRepository PersonalityGroup
        {
            get
            {
                if(_pGroup == null)
                {
                    _pGroup = new PersonalityGroupRepository(_context);
                }
                return _pGroup;
            }
        }

        public IMajorPGroupRepository MajorPgroup
        {
            get
            {
                if(_pGroupMajor == null)
                {
                    _pGroupMajor = new MajorPGroupRepository(_context);
                }
                return _pGroupMajor;
            }
        }

        public IPgroupAnswerRepository AnswerPGroup
        {
            get
            {
                if(_pGroupAnswer == null)
                {
                    _pGroupAnswer = new PgroupAnswerRepository(_context);
                }
                return _pGroupAnswer;
            }
        }

        public ISubjectGroupMajorRepository SubjectGroupMajor
        {
            get
            {
                if(_subGroup == null)
                {
                    _subGroup = new SubjectGroupMajorRepository(_context);
                }
                return _subGroup;
            }
        }

        public ISubjectGroupSubjectRepository SubjectGroupSubject
        {
            get
            {
                if(_subGroupSub == null)
                {
                    _subGroupSub = new SubjectGroupSubjectRepository(_context);
                }
                return _subGroupSub;
            }
        }

        public IUserSubjectGroupRepository UserSubjectGroup
        {
            get
            {
                if (_userSubGroups == null)
                {
                    _userSubGroups = new UserSubjectGroupRepository(_context);
                }
                return _userSubGroups;
            }
        }

        public IUserSubjectRepository UserSubject
        {
            get
            {
                if(_userSubs == null)
                {
                    _userSubs = new UserSubjectRepository(_context);
                }
                return _userSubs;
            }
        }

        public IMajorCollegesRepository MajorColleges
        {
            get
            {
                if(_majorCol == null)
                {
                    _majorCol = new MajorCollegesRepository(_context);
                }
                return _majorCol;
            }
        }

        public IUserMajorRepository MajorUser
        {
            get
            {
                if (_userMajor == null)
                {
                    _userMajor = new UserMajorRepository(_context);
                }
                return _userMajor;
            }
        }

        public IMajorSubjectGroupCollegesRepository MajorSubjectGroupColleges
        {
            get
            {
                if(_majorSubjectGroupColleges == null)
                {
                    _majorSubjectGroupColleges = new MajorSubjectGroupCollegesRepository(_context);
                }
                return _majorSubjectGroupColleges;
            }
        }

        public ILessionMajorRepository LessionMajor
        {
            get
            {
                if(_lession == null)
                {
                    _lession = new LessionMajorRepository(_context);
                }
                return _lession;
            }
        }

        public ISubjectRepository Subject
        {
            get
            {
                if(_subject == null)
                {
                    _subject = new SubjectRepository(_context);
                }
                return _subject;
            }
        }

        public ICollegesRepository Colleges
        {
            get
            {
                if (_colleges == null)
                    _colleges = new CollegesRepository(_context);
                return _colleges;
            }
        }

        public IMajorRepository Major
        {
            get
            {
                if(_major == null)
                {
                    _major = new MajorRepository(_context);
                }
                return _major;
            }
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
