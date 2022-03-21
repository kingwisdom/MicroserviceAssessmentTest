using BoardService.Data;
using BoardService.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoardService.Repository.Implementation
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {


        private DatabContext _databContext;
        private DbSet<TEntity> _dbSet;
        public Repository(DatabContext databContext)
        {
            _databContext = databContext;
            _dbSet = _databContext.Set<TEntity>();
        }
        public async Task AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> where)
        {
            if (where == null)
            {
                throw new ArgumentNullException("entity");
            }
            IEnumerable<TEntity> objects = _dbSet.Where<TEntity>(where).AsEnumerable();
            foreach (TEntity obj in objects)
                _dbSet.Remove(obj);
        }

        public async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> where)
        {
            if (where != null)
                return await _dbSet.Where(where).FirstOrDefaultAsync<TEntity>();
            else
                return await _dbSet.SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<TEntity>> GetManyAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public async Task<List<TEntity>> GetManyListAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _dbSet.Where(where).ToListAsync();
        }

        public async Task InsertRangeAsync(IEnumerable<TEntity> entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public void Update(TEntity entity)
        {
            if (entity != null)
            {
                _databContext.Entry(entity).State = EntityState.Detached;
            }
            _dbSet.Attach(entity);
            _databContext.Entry(entity).State = EntityState.Modified;
        }

        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_databContext != null)
                {
                    _databContext.Dispose();
                    _databContext = null;
                }
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~Repository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
