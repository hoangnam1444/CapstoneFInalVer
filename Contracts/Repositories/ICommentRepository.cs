using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface ICommentRepository
    {
        void Create(Comment comment);
        Task<List<BlogInList>> GetNumOfComment(List<BlogInList> blogs);
        Task<BlogDetail> GetNumOfComment(BlogDetail blog);
    }
}