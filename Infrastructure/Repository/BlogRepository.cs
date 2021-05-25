using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        Task<Blog> GetAsyncWithPosts(long blogId);
    }

    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly BloggingContext _context;

        public BlogRepository(BloggingContext context) : base(context)
        {
            _context = context;
        }

        public override IAsyncEnumerable<Blog> GetAllAsync()
        {
            return _context.Blogs
                .Include(blog => blog.Posts)
                .ToAsyncEnumerable();
        }

        public async Task<Blog> GetAsyncWithPosts(long blogId)
        {
            return await _context.Blogs
                .Include(blog => blog.Posts)
                .FirstOrDefaultAsync(b => b.Id == blogId);
        }
    }
}