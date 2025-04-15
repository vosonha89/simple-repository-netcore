using Microsoft.EntityFrameworkCore;
using DevNetCore.SimpleRepository.Abstract;
using DevNetCore.SimpleRepository.Interface;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DevNetCore.SimpleRepository.Implementation
{
    /// <summary>
    /// Simple repository
    /// </summary>
    public class SimpleRepository : ISimpleRepository, IDisposable
    {
        private readonly DbContext _dbContext;

        public DbContext DbContext => _dbContext;

        /// <summary>
        /// Constructor with dbContext
        /// </summary>
        /// <param name="dbContext"></param>
        protected SimpleRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Get list data
        /// </summary>
        /// <param name="predicate"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : DbEntity, new()
        {
            return _dbContext.Set<T>().Where(predicate);
        }

        /// <summary>
        /// Get one data
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public Task<T?> Get<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
            where T : DbEntity, new()
        {
            return _dbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Insert one
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ValueTask<EntityEntry<T>> Insert<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            entity.Id = Guid.NewGuid();
            if (string.IsNullOrEmpty(entity.CreatedBy))
            {
                entity.CreatedBy = Guid.Empty.ToString();
            }

            entity.CreatedDate = DateTime.UtcNow;
            return _dbContext.Set<T>().AddAsync(entity, cancellationToken);
        }

        /// <summary>
        /// Update one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public EntityEntry<T> Update<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            if (string.IsNullOrEmpty(entity.LastUpdateBy))
            {
                entity.LastUpdateBy = Guid.Empty.ToString();
            }
            entity.LastUpdateDate = DateTime.UtcNow;
            return _dbContext.Set<T>().Update(entity);
        }

        /// <summary>
        /// Detele one
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public EntityEntry<T> Delete<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            return _dbContext.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Submit change all transactions
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> SaveChanges(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose action
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            _dbContext.Dispose();
        }
    }
}