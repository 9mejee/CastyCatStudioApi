using DAL.Interfaces;
using DomainEntities;
using DTO.Customer;
namespace DAL.Repositeries
{
    public class OrderProductsServices : IOrderProductsServices
    {
        private readonly IGenericService<OrderProducts> _repository;

        public OrderProductsServices(IGenericService<OrderProducts> customerOrderProductRepo)
        {
            this._repository = customerOrderProductRepo;
        }

        #region public methods

        public async Task AddAsync(OrderProducts orderProduct)
        {
            try
            {
                await _repository.AddWithSaveAsync(orderProduct);
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region private methods

        /// <summary>
        /// Convert into dto form
        /// </summary>
        /// <param name="dbCustomer"></param>
        /// <returns></returns>
        private static CustomerDto SetDto(Customer dbCustomer)
        {
            return new CustomerDto
            {
                Id = dbCustomer.Id,
                Name = dbCustomer.Name
            };
        }

        /// <summary>
        /// Set entity
        /// </summary>
        /// <param name="customerDto"></param>
        /// <param name="dbCustomer"></param>
        /// <returns></returns>
        private static Customer SetEntity(CustomerDto customerDto)
        {
            return new Customer()
            {
                Id = customerDto.Id,
                Name = customerDto.Name
            };
        }

        #endregion
    }
}