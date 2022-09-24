using Contracts.HandleServices;
using Contracts.Repositories;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeature;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MajorTestOrientation.Controllers
{
    [Route("api/blogs")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly IUserAccessor _userAccessor;

        public BlogsController(IRepositoryManager repository, IUserAccessor userAccessor)
        {
            _repository = repository;
            _userAccessor = userAccessor;
        }

        /// <summary>
        /// Role: Admin (New blog)
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> NewBLog(NewBlogParam param)
        {
            var role = _userAccessor.GetAccountRole();

            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permision");
            }

            var newBlogs = new Blog
            {
                CreatedDate = DateTime.UtcNow,
                Description = param.Description,
                Image = param.Image,
                Title = param.Title
            };

            _repository.Blog.Create(newBlogs);

            newBlogs.Users.Add(new User_Blog
            {
                IsOwner = true,
                IsReacted = false,
                UserId = _userAccessor.GetAccountId()
            });

            await _repository.SaveAsync();
            return Ok("Save changes success");
        }

        /// <summary>
        /// Role: All (get list blog)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetListBlog()
        {
            var blogs = await _repository.Blog.GetList();
            blogs = await _repository.UserBlog.GetReacted(blogs, _userAccessor.GetAccountId());
            blogs = await _repository.Comment.GetNumOfComment(blogs);
            return Ok(blogs);
        }

        /// <summary>
        /// Role: All (get list blog)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{blog_id}/detail")]
        public async Task<IActionResult> GetBlogDetail(int blog_id)
        {
            var blog = await _repository.Blog.GetDetail(blog_id);
            blog = await _repository.UserBlog.GetReacted(blog, _userAccessor.GetAccountId());
            blog = await _repository.Comment.GetNumOfComment(blog);
            blog = await _repository.UserBlog.GetUserForBlog(blog);
            return Ok(blog);
        }

        /// <summary>
        /// Role: All (React blog)
        /// </summary>
        /// <param name="blog_id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{blog_id}/react")]
        public async Task<IActionResult> ReactBlog(int blog_id)
        {
            var blog = await _repository.UserBlog.GetByBlogId(blog_id, _userAccessor.GetAccountId());

            if (blog == null)
            {
                _repository.UserBlog.Create(new User_Blog
                {
                    BlogId = blog_id,
                    IsOwner = false,
                    IsReacted = true,
                    UserId = _userAccessor.GetAccountId()
                });
            }
            else
            {
                if (blog.IsReacted == false)
                {
                    blog.IsReacted = true;
                }
                else
                {
                    blog.IsReacted = false;
                }
                _repository.UserBlog.Update(blog);
            }

            await _repository.SaveAsync();

            return Ok("Save success");
        }

        /// <summary>
        /// Role: All (create comment)
        /// </summary>
        /// <param name="blog_id"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("{blog_id}/comment")]
        public async Task<IActionResult> CreateComment(int blog_id, NewComment comment)
        {
            _repository.Comment.Create(new Comment
            {
                BlogId = blog_id,
                Content = comment.Content,
                UserId = _userAccessor.GetAccountId(),
                CreatedDate = DateTime.UtcNow
            });

            await _repository.SaveAsync();
            return Ok("Save change success");
        }

        /// <summary>
        /// Role: Admin (update blog)
        /// </summary>
        /// <param name="blog_id"></param>
        /// <param name="blogInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{blog_id}")]
        public async Task<IActionResult> UpdateBlog(int blog_id, NewBlogParam blogInfo)
        {
            var role = _userAccessor.GetAccountRole();

            if (role != 2)
            {
                throw new ErrorDetails(System.Net.HttpStatusCode.BadRequest, "Don't have permision");
            }

            var blog = await _repository.Blog.GetById(blog_id);

            blog.Description = blogInfo.Description;
            blog.Image = blogInfo.Image;
            blog.Title = blogInfo.Title;

            _repository.Blog.Update(blog);

            await _repository.SaveAsync();

            return Ok("Save changes success");
        }
    }
}
