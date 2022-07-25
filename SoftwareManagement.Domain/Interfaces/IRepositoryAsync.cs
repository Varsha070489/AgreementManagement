using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareManagement.Domain.Interfaces
{
    public interface IRepositoryAsync<T> where T : class
    {
        IQueryable<T> Entities { get; }

        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression, string[] IncludeParam);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        Task<T> GetByIdAsync(int id, string[] IncludeParam = null);

        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(string[] IncludeParam);

        Task<List<T>> GetPagedReponseAsync(int pageNumber, int pageSize);

        Task<T> AddAsync(T entity);
        Task<List<T>> AddBulkAsync(List<T> entity);
        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
        Task SqlForAsync(string sql, params object[] ss);

    }
}
