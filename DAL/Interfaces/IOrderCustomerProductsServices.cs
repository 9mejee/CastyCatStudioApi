using DomainEntities;

namespace DAL.Interfaces
{
    public interface IOrderProductsServices
    {

        /// <summary>
        /// Add Customer
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task AddAsync(OrderProducts orderProduct);
    }
}