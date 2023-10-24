using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IGenericService<T> where T : class
    {
        /// <summary>
        /// Add record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// update record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void Update(T entity);

        /// <summary>
        /// Add record with save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> AddWithSaveAsync(T entity);

        /// <summary>
        /// Update with save
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> UpdateWithSaveAsync(T entity);

        /// <summary>
        /// Save
        /// </summary>
        /// <returns></returns>
        Task SaveAsync();

        /// <summary>
        /// Delete record
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task DeleteWithSaveAsync(T entity);

        /// <summary>
        /// Get record with sepcified condition
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Get record by primary key id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        Task<T> GetAsync(int id);

        /// <summary>
        /// Get List
        /// </summary>
        /// <returns></returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Has any record
        /// </summary>
        /// <returns></returns>
        Task<bool> IsAny(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Fetch lsit of records
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        IQueryable<T> GetAll(List<string> thanInclude, params Expression<Func<T, object>>[] includes);
    }
}