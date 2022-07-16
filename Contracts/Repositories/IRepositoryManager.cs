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
        IPgroupAnswerRepository AnswerPGroup { get; }
        ISubjectGroupMajorRepository SubjectGroupMajor { get; }
        ISubjectGroupSubjectRepository SubjectGroupSubject { get; }
        IUserSubjectGroupRepository UserSubjectGroup { get; }
        Task SaveAsync();
    }
}
