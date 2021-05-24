using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Web.Controllers
{
    public class BlogController : BaseApiController
    {
        private readonly IBlogRepository _blogRepository;
        private readonly ILogger<BlogController> _logger;

        public BlogController(IBlogRepository blogRepository, ILogger<BlogController> logger)
        {
            _blogRepository = blogRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Blog>> Get()
        {
            return await _blogRepository.GetAllAsync().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Blog> GetById(long id)
        {
            _logger.LogInformation("Received param {Id}", id);
            return await _blogRepository.GetAsyncWithPosts(id);
        }

        [HttpPost]
        public async Task<Blog> Post([FromBody] Blog blog)
        {
            if (blog.Url == null || blog.Url.Equals(string.Empty))
            {
                throw new ArgumentNullException(nameof(blog.Url));
            }

            return await _blogRepository.InsertAsync(blog);
        }
    }
}