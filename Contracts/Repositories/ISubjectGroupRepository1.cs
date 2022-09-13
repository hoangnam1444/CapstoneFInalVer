using Entities.DataTransferObject;
using Entities.DTOs;
using Entities.RequestFeature;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ISubjectGroupRepository
    {
        public Task<Pagination<SubjectGroupReturn>> GetAll(PagingParameters param); 
    }
}