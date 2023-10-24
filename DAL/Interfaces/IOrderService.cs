using DTO.OrderRequest;

namespace DAL.Interfaces
{
    public interface IOrderService
    {
        /// <summary>
        /// Add Order with customer products
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        Task AddWithCustomerProductsAsync(RequestOrderCustomerProductsDto dto);

        /// <summary>
        /// Get all orders against customer id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        Task<ResponseOrderDto> GetAllOrdersAgainstCustomerAsyc(int customerId);

        /// <summary>
        /// Get all orders 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        IQueryable<ResponseOrderDto> List();
    }
}