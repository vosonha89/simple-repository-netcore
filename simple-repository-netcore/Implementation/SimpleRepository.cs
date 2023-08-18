using Microsoft.EntityFrameworkCore;
using DevNetCore.SimpleRepository.Abstract;
using DevNetCore.SimpleRepository.Interface;
using System.Linq.Expressions;

namespace DevNetCore.SimpleRepository.Implementation
{
    public partial class SimpleRepository : ISimpleRepository, IDisposable
    {
        public readonly DbContext _DbContext;

        public SimpleRepository(DbContext dbContext)
        {
            _DbContext = dbContext;
        }

        public IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : DbEntity, new()
        {
            return _DbContext.Set<T>().Where(predicate);
        }

        public Task<T?> Get<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            return _DbContext.Set<T>().FirstOrDefaultAsync(predicate, cancellationToken);
        }

        public Task<int> Insert<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            entity.Id = Guid.NewGuid();
            if (string.IsNullOrEmpty(entity.CreatedBy))
            {
                entity.CreatedBy = Guid.Empty.ToString();
            }
            entity.CreatedDate = DateTime.UtcNow;
            _DbContext.Set<T>().AddAsync(entity, cancellationToken);
            return _DbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<int> Update<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            if (string.IsNullOrEmpty(entity.LastUpdateBy))
            {
                entity.LastUpdateBy = Guid.Empty.ToString();
            }
            entity.LastUpdateDate = DateTime.UtcNow;
            _DbContext.Set<T>().Update(entity);
            return _DbContext.SaveChangesAsync(cancellationToken);
        }

        public Task<int> Delete<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            _DbContext.Set<T>().Remove(entity);
            return _DbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
