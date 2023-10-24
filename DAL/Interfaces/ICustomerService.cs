using DomainEntities;
using DTO.Customer;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface ICustomerService
    {

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<CustomerDto> AddAsync(CustomerDto dto);

        /// <summary>
        /// Update Customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<CustomerDto> UpdateAsync(CustomerDto dto);

        /// <summary>
        /// Delete Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(CustomerDto dto);

        /// <summary>
        /// Get Customer
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        Task<CustomerDto> GetAsync(Expression<Func<Customer, bool>> predicate);

        /// <summary>
        /// Get by Id of Customer
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CustomerDto> GetByIdAsync(int id);

        /// <summary>
        /// Get List of Customers
        /// </summary>
        /// <returns></returns>
        IEnumerable<CustomerDto> List();
        /// <summary>
        /// Check either is Customer already exists or not
        /// </summary>
        /// <returns></returns>
        Task<bool> IsExists(CustomerDto dto);
    }
}