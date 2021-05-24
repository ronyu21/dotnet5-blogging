using System.Collections.Generic;
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
        private readonly BloggingContext context;

        public BlogRepository(BloggingContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await context.Blogs.ToListAsync();
        }

        public async Task<Blog> GetAsync(int blogId)
        {
            return await context.Blogs.FindAsync(blogId);
        }

        public async Task<Blog> CreateAsync(Blog blog)
        {
            var created = context.Blogs.Add(blog);
            await context.SaveChangesAsync();
            return created.Entity;
        }

        public async Task<Blog> UpdateAsync(Blog blog)
        {
            var updated = context.Blogs.Update(blog);
            await context.SaveChangesAsync();
            return updated.Entity;
        }

        public async Task DeleteAsync(int blogId)
        {
            context.Blogs.Remove(new Blog
            {
                BlogId = blogId
            });

            await context.SaveChangesAsync();
        }
    }
}