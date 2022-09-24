using Entities.DTOs;
using Entities.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Contracts.Repositories
{
    public interface IUserBLogRepository
    {
        void Create(User_Blog userBlog);
        void Update(User_Blog user_Blog);
        Task<User_Blog> GetByBlogId(int blogId, int user_id);
        Task<List<BlogInList>> GetReacted(List<BlogInList> blogs, int user_id);
        Task<BlogDetail> GetReacted(BlogDetail blog, int userId);
        Task<BlogDetail> GetUserForBlog(BlogDetail blog);
    }
}
