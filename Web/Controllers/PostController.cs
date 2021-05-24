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
    public class PostController : BaseApiController
    {
        private readonly IRepository<Post> _postRepository;
        private readonly ILogger<PostController> _logger;

        public PostController(IRepository<Post> postRepository, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<Post>> Get()
        {
            return await _postRepository.GetAllAsync().ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<Post> GetById(long id)
        {
            _logger.LogInformation("Received param {Id}", id);
            return await _postRepository.GetAsync(id);
        }

        [HttpPost]
        public async Task<Post> Post([FromBody] Post post)
        {
            if (post.BlogId == null)
            {
                throw new ArgumentNullException(nameof(post.BlogId));
            }

            if (post.Title == null || post.Title.Equals(string.Empty))
            {
                throw new ArgumentNullException(nameof(post.Title));
            }

            if (post.Content == null || post.Content.Equals(string.Empty))
            {
                throw new ArgumentNullException(nameof(post.Content));
            }

            return await _postRepository.InsertAsync(post);
        }
    }
}