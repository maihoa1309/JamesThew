﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project3.Data;
using Project3.Models;
using System.Linq.Expressions;

namespace Project3.Repository
{
    public interface IBaseRepository<T> where T : Base
    {
        Task<List<T>> FilterAsync(
            Expression<Func<T, object>> includeProperty = null, //Su dung de include .Include(r => r....)
            Expression<Func<T, bool>> filter = null, //Su dung de tim kiem (.Where(r => r....))

            string columnName = "id",
            bool columnASC = false,
            int index = 1,
            int size = 10

            );
        Task<List<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        T FindById(int id);


    }
    public class BaseRepository<T> : IBaseRepository<T> where T : Base
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly UserManager<CustomUser> _userManager;
        protected readonly string currentUserId = "";
        protected readonly IHttpContextAccessor _contextAccessor;
        public BaseRepository(ApplicationDbContext context, UserManager<CustomUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _dbSet = context.Set<T>();
            _userManager = userManager;
            _contextAccessor = contextAccessor;
            var currentUser = _userManager.GetUserAsync(_contextAccessor.HttpContext.User).GetAwaiter().GetResult();

            if (currentUser != null)
            {
                this.currentUserId = currentUser.Id;
            }
        }
        public async Task<T> CreateAsync(T entity)
        {
            if (entity != null)
            {
                entity.IsDeleted= false;
                entity.CreatedTime = DateTime.Now;
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
        public async Task<List<T>> FilterAsync(
           Expression<Func<T, object>> includeProperty = null,
           Expression<Func<T, bool>> filter = null,
           string columnName = "Id",
           bool columnASC = false,
           int index = 1,
           int size = 10)
        {
            var dataRows = _dbSet.Where(r => r.IsDeleted != true).AsQueryable();

            if (includeProperty != null)
            {
                dataRows = dataRows.Include(includeProperty);
            }

            if (filter != null)
            {
                dataRows = dataRows.Where(filter);
            }

            //total = dataRows.Count();


            dataRows = dataRows.Skip((index - 1) * size).Take(size);

            var propertyInfo = typeof(T).GetProperty(columnName);
            var parameter = Expression.Parameter(typeof(T), "p");
            var property = Expression.Property(parameter, propertyInfo);
            var lambda = Expression.Lambda<Func<T, object>>(Expression.Convert(property, typeof(object)), parameter);

            if (!columnASC)
            {
                dataRows = dataRows.OrderByDescending(lambda);
            }
            else
            {
                dataRows = dataRows.OrderBy(lambda);
            }
            var result = await dataRows.ToListAsync();
            return result;

        }

        public T FindById(int id)
        {
            var result =  _dbSet.Find(id);
            return result;
        }
    }
}
