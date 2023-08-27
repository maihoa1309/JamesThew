using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;

namespace Project3.Repository
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);

    }
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }


        public async Task<T> CreateAsync(T entity)
        {
            if (entity != null)
            {
                await _dbSet.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }


        public async Task<List<T>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity != null)
            {
                _dbSet.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            return null;
        }
    }
}
