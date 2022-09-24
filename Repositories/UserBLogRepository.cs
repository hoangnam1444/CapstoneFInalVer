using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserBLogRepository : RepositoryBase<User_Blog>,IUserBLogRepository
    {
        public UserBLogRepository(DataContext context) : base(context)
        {

        }

        public async Task<User_Blog> GetByBlogId(int blogId, int userId)
        {
            return await FindByCondition(x => x.BlogId == blogId && x.UserId == userId, true).FirstOrDefaultAsync();
        }

        public async Task<List<BlogInList>> GetReacted(List<BlogInList> blogs, int userId)
        {
            var result = new List<BlogInList>();
            foreach(var blog in blogs)
            {
                blog.IsReacted = await FindByCondition(x => x.UserId == userId && x.BlogId == blog.Id && x.IsReacted == true, true).FirstOrDefaultAsync() != null;
                blog.NumOfReact = await FindByCondition(x => x.BlogId == blog.Id && x.IsReacted == true, true).CountAsync();
                result.Add(blog);
            }
            return result;
        }

        public async Task<BlogDetail> GetReacted(BlogDetail blog, int userId)
        {
            blog.IsReacted = await FindByCondition(x => x.UserId == userId && x.BlogId == blog.BlogId && x.IsReacted == true, true).FirstOrDefaultAsync() != null;
            blog.NumOfReact = await FindByCondition(x => x.BlogId == blog.BlogId && x.IsReacted == true, true).CountAsync();
            return blog;
        }

        public async Task<BlogDetail> GetUserForBlog(BlogDetail blog)
        {
            var blogOwner = await FindByCondition(X => X.BlogId == blog.BlogId && X.IsOwner == true, false).Include(x => x.UserId).FirstOrDefaultAsync();

            blog.OwnerAvatar = blogOwner.User.ImagePath;
            blog.OwnerId = blogOwner.UserId;
            blog.OwnerName = blogOwner.User.UserName;

            return blog;
        }
    }
}