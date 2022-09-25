using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    internal class CommentRepository : RepositoryBase<Comment>,ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<CommentReturn>> GetAllComment(int blog_id)
        {
            return await FindByCondition(x => x.BlogId == blog_id, true)
                .Include(x => x.User)
                .Select(x => new CommentReturn
            {
                CreatedDate = x.CreatedDate,
                Id = x.Id,
                OwnerAvatar = x.User.ImagePath,
                OwnerId = x.UserId,
                OwnerName = x.User.UserName,
                Content = x.Content,
            }).ToListAsync();
        }

        public async Task<List<BlogInList>> GetNumOfComment(List<BlogInList> blogs)
        {
            var result = new List<BlogInList>();
            foreach (var blog in blogs)
            {
                blog.NumOfComment = await FindByCondition(x => x.BlogId == blog.Id, true).CountAsync();
                result.Add(blog);
            }
            return result;
        }

        public async Task<BlogDetail> GetNumOfComment(BlogDetail blog)
        {
            blog.NumOfComment = await FindByCondition(x => x.BlogId == blog.BlogId, true).CountAsync();
            return blog;
        }
    }
}