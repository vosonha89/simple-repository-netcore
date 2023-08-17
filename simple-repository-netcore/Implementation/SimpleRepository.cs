using Microsoft.EntityFrameworkCore;
using NetCore.SimpleRepository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.SimpleRepository.Implementation
{
    public partial class SimpleRepository : IDisposable
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

        public int Update<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new()
        {
            if (string.IsNullOrEmpty(entity.LastUpdateBy))
            {
                entity.LastUpdateBy = Guid.Empty.ToString();
            }
            entity.LastUpdateDate = DateTime.UtcNow;
        }

        public void Dispose()
        {
            _DbContext.Dispose();
        }
    }
}
