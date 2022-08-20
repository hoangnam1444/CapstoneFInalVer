using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IRepositoryManager
    {
        ISysUserRepository SysUser { get; }
        ISecurityCodeRepository SecurityCode { get; }
        IQuestionRepository Question { get; }
        IAnswerRepository Answer { get; }
        ITestResultRepository TestResult { get; }
        ITestDeclarationRepository Test { get; }
        ITestTypeRepository TestType { get; }
        IPersonalityGroupRepository PersonalityGroup { get; }
        IMajorPGroupRepository MajorPgroup { get; }
        IMajorCollegesRepository MajorColleges { get; }
        IPgroupAnswerRepository AnswerPGroup { get; }
        ISubjectGroupMajorRepository SubjectGroupMajor { get; }
        ISubjectGroupSubjectRepository SubjectGroupSubject { get; }
        IUserSubjectGroupRepository UserSubjectGroup { get; }
        IUserSubjectRepository UserSubject { get; }
        IUserMajorRepository MajorUser { get; }
        IMajorSubjectGroupCollegesRepository MajorSubjectGroupColleges { get; }
        ILessionMajorRepository LessionMajor { get; }
        ISubjectRepository Subject { get; }
        ICollegesRepository Colleges { get; }
        IMajorRepository Major { get; }
        IUserCollegeRepository UserCollege { get; }
        IChatRoomRepository ChatRoom { get; }
        Task SaveAsync();
    }
}
