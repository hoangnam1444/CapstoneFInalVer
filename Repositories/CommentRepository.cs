using Contracts.Repositories;
using Entities;
using Entities.DTOs;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    internal class CommentRepository : RepositoryBase<Comment>,ICommentRepository
    {
        public CommentRepository(DataContext context) : base(context)
        {
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