﻿using CarRentals.Entities;
using System.Linq.Expressions;

namespace CarRentals.Repository.Interface
{
    public interface IRepository<T> where T : BaseEntity, new()
    {
        Task<T> CreateAsync(T entity);
        Task<T> GetAsync(string id);
        Task<T> GetAsync(Expression<Func<T, bool>> expression);
        Task<T> UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task<List<T>> GetAllByIdsAsync(List<string> ids);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
        Task<bool> ExistsAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> SelectAll();
        Task<IEnumerable<T>> SelectAll(Expression<Func<T, bool>> expression = null);
    }
}
