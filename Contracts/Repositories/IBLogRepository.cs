using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IBLogRepository
    {
        void Create(Blog blog);
        void Update(Blog blog);
        Task<Blog> GetById(int blog_id);
        Task<List<BlogInList>> GetList();
        Task<BlogDetail> GetDetail(int blog_id);
    }
}