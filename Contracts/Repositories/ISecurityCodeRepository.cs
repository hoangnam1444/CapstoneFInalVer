using Entities.Models;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISecurityCodeRepository
    {
        Task<SecurityCode> Create(int userId);
        Task<bool> ActivatedCode(string code, int userId);
    }
}
