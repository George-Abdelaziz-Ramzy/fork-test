using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EliteTest.Application.Interfaces;
public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetFirstWithIncludeAsync(Expression<Func<T, bool>> where, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> conditions);
    Task<IEnumerable<T>> FindWithIncludeAsync(Expression<Func<T, bool>> conditions, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null);
    Task AddAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
}
