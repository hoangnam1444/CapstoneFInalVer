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
    public class BlogRepository : RepositoryBase<Blog>, IBLogRepository
    {
        public BlogRepository(DataContext context) : base(context)
        {
        }

        public async Task<Blog> GetById(int blog_id)
        {
            return await FindByCondition(x => x.Id == blog_id, true).FirstOrDefaultAsync();
        }

        public async Task<BlogDetail> GetDetail(int blog_id)
        {
            var detail = await FindByCondition(x => x.Id == blog_id, true)
                .Select(x => new BlogDetail
                {
                    BlogId = x.Id,
                    BlogImage = x.Image,
                    Description = x.Description,
                    Title = x.Title
                }).FirstOrDefaultAsync();
            return detail;
        }

        public async Task<List<BlogInList>> GetList()
        {
            var count = await FindAll(true).CountAsync();
            if(count > 100)
            {
                count = count - (count - 100);
            }
            else
            {
                count = 0;
            }
            var result = await FindAll(true).Skip(count).Select(x => new BlogInList
            {
                Description = x.Description,
                Image = x.Image,
                Title = x.Title
            }).ToListAsync();

            return result;
        }
    }
}
