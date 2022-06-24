using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ITestTypeRepository
    {
        Task<List<TypeForGetAll>> GetAllType();
        Task Update(int type_id, UpdateType info);
        Task<TestTypes> GetById(int typeId);
    }
}
