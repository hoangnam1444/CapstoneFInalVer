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
        Task SaveAsync();
    }
}
