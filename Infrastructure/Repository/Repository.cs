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

        private readonly DbSet<T> _entities;

        public Repository(BloggingContext context)
        {
            _context = context;

            _entities = context.Set<T>();
        }

        public virtual IAsyncEnumerable<T> GetAllAsync()
        {
            return _entities.AsAsyncEnumerable();
        }

        public virtual async Task<T> GetAsync(long id)
        {
            return await _entities.SingleOrDefaultAsync(entity => entity.Id == id);
        }

        public virtual async Task<T> InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var created = _entities.Add(entity);
            await _context.SaveChangesAsync();

            return created.Entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var updated = _entities.Update(entity);
            await _context.SaveChangesAsync();

            return updated.Entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}