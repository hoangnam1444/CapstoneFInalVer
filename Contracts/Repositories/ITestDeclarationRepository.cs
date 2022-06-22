using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITestDeclarationRepository
    {
        Task<TestDeclarations> GetById(int id);
        Task<List<TestToUpdateQuestion>> GetAllTest();
    }
}
