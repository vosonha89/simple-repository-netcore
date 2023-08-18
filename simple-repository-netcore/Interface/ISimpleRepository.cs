using DevNetCore.SimpleRepository.Abstract;
using System.Linq.Expressions;

namespace DevNetCore.SimpleRepository.Interface
{
    public interface ISimpleRepository
    {
        /// <summary>
        /// Get list data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<T> GetList<T>(Expression<Func<T, bool>> predicate) where T : DbEntity, new();
        /// <summary>
        /// Get single data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T?> Get<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default) where T : DbEntity, new();
        /// <summary>
        /// Insert single data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> Insert<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new();
        /// <summary>
        /// Update single data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> Update<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new();
        /// <summary>
        /// Delete single data
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> Delete<T>(T entity, CancellationToken cancellationToken = default) where T : DbEntity, new();
    }
}
