using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public interface IBlogRepository : IRepository<Blog>
    {
        // Task<IEnumerable<Blog>> GetAllAsync();

        Task<Blog> GetAsyncWithPosts(long blogId);

        // Task<Blog> CreateAsync(Blog blog);
        //
        // Task<Blog> UpdateAsync(Blog blog);
        //
        // Task DeleteAsync(int blogId);
    }

    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        private readonly BloggingContext _context;

        public BlogRepository(BloggingContext context) : base(context)
        {
            _context = context;
        }

        // public async Task<IEnumerable<Blog>> GetAllAsync()
        // {
        //     return await _context.Blogs
        //         .Include(blog => blog.Posts)
        //         .ToListAsync();
        // }

        public async Task<Blog> GetAsyncWithPosts(long blogId)
        {
            return await _context.Blogs
                .Include(blog => blog.Posts)
                .FirstOrDefaultAsync(b => b.Id == blogId);
        }

        // public async Task<Blog> CreateAsync(Blog blog)
        // {
        // var created = _context.Blogs.Add(blog);
        // await _context.SaveChangesAsync();
        // return created.Entity;
        // }

        // public async Task<Blog> UpdateAsync(Blog blog)
        // {
        //     var updated = _context.Blogs.Update(blog);
        //     await _context.SaveChangesAsync();
        //     return updated.Entity;
        // }
        //
        // public async Task DeleteAsync(int blogId)
        // {
        //     _context.Blogs.Remove(new Blog
        //     {
        //         Id = blogId
        //     });
        //
        //     await _context.SaveChangesAsync();
        // }
    }
}