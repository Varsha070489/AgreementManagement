using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using SoftwareManagement.Domain.Interfaces;
using SoftwareManagement.Infrastructure.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Infrastructure.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly ApplicationDbContext _dbContext;

        public RepositoryAsync(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression).AsNoTracking();
        }


        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression, string[] IncludeParam)
        {
            var data = _dbContext.Set<T>();
            foreach (var param in IncludeParam)
            {
                data.AddRange(data.Include(param));
            }
            return data.Where(expression);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<List<T>> AddBulkAsync(List<T> entity)
        {
            await _dbContext.BulkInsertAsync(entity);
            return entity;
        }
        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<List<T>> GetAllAsync()
        {

            return await _dbContext.Set<T>().ToListAsync();

        }

        public async Task<List<T>> GetAllAsync(string[] IncludeParam)
        {
            var data = _dbContext.Set<T>();

            foreach (var param in IncludeParam)
            {
                data.AddRange(data.Include(param));
            }

            return await data.ToListAsync();

        }

        public async Task<T> GetByIdAsync(int id, string[] IncludeParam)
        {

            var data = _dbContext.Set<T>();
            if (IncludeParam != null)
                foreach (var param in IncludeParam)
                {
                    data.AddRange(data.Include(param));
                }

            return await data.FindAsync(id);
        }



        public async Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> expression)
        {
            return _dbContext.Set<T>().Where(expression);
        }

        public Task SqlForAsync(string sql, params object[] parameters)
        {
            _dbContext.Set<T>().FromSqlRaw(sql, parameters);
            return Task.CompletedTask;
        }
    }
}
