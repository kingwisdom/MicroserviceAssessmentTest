﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoardService.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task InsertRangeAsync(IEnumerable<T> entity);
        Task AddAsync(T entity);
        void Update(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> where);
        void Delete(Expression<Func<T, bool>> where);
        Task<IEnumerable<T>> GetManyAsync(Expression<Func<T, bool>> where);
        Task<List<T>> GetManyListAsync(Expression<Func<T, bool>> where);
    }
}
