using DomainEntities;
using DTO.Product;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IProductService
    {
        /// <summary>
        /// Add Product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ProductDto> AddAsync(ProductDto dto);

        /// <summary>
        /// Update Product
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ProductDto> UpdateAsync(ProductDto dto);

        /// <summary>
        /// Delete product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(ProductDto dto);

        /// <summary>
        /// Get Product
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<ProductDto> GetAsync(Expression<Func<Product, bool>> predicate);

        /// <summary>
        /// Get by Id of Product
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ProductDto> GetByIdAsync(int id);

        /// <summary>
        /// Get List of Products
        /// </summary>
        /// <returns></returns>
        IEnumerable<ProductDto> List();

        /// <summary>
        /// Check either is product already exists or not
        /// </summary>
        /// <returns></returns>
        Task<bool> IsExists(ProductDto dto);
    }
}