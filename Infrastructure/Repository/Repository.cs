using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<T> GetAsync(long id);
        Task<T> InsertAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }

    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly BloggingContext _context;

        private DbSet<T> entities;

        public Repository(BloggingContext context)
        {
            _context = context;

            entities = context.Set<T>();
        }

        public IAsyncEnumerable<T> GetAllAsync()
        {
            return entities.AsAsyncEnumerable();
        }

        public async Task<T> GetAsync(long id)
        {
            return await entities.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var created = entities.Add(entity);
            await _context.SaveChangesAsync();

            return created.Entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var updated = entities.Update(entity);
            await _context.SaveChangesAsync();

            return updated.Entity;
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}