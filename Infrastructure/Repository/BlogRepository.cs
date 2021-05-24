using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> GetAllAsync();

        Task<Blog> GetAsync(int blogId);

        Task<Blog> CreateAsync(Blog blog);

        Task<Blog> UpdateAsync(Blog blog);

        Task DeleteAsync(int blogId);
    }

    public class BlogRepository : IBlogRepository
    {
        private readonly BloggingContext _context;

        public BlogRepository(BloggingContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _context.Blogs
                .Include(blog => blog.Posts)
                .ToListAsync();
        }

        public async Task<Blog> GetAsync(int blogId)
        {
            return await _context.Blogs
                .Include(blog => blog.Posts)
                .FirstOrDefaultAsync(b => b.BlogId == blogId);
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            var created = _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            var updated = _context.Blogs.Update(blog);
            await _context.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteAsync(int blogId)
        {
            _context.Blogs.Remove(new Blog
            {
                BlogId = blogId
            });

            await _context.SaveChangesAsync();
        }
    }
}