using DAL.Interfaces;
using DomainEntities;
using DTO.OrderRequest;
using DTO.Product;

namespace DAL.Repositeries
{
    public class OrderServices : IOrderService
    {
        private readonly IGenericService<Order> _orderRepo;
        private readonly IOrderProductsServices _orderCustomerProductsServices;

        public OrderServices(IGenericService<Order> orderRepo, IOrderProductsServices orderCustomerProductsServices)
        {
            _orderRepo = orderRepo;
            _orderCustomerProductsServices = orderCustomerProductsServices;
        }

        #region public methods

        public async Task AddWithCustomerProductsAsync(RequestOrderCustomerProductsDto dto)
        {
            try
            {
                var order = new Order { CustomerId = dto.Customer.Id };
                await _orderRepo.AddWithSaveAsync(order);

                foreach (var product in dto.Customer.Products)
                {
                    if (product.Id > 0)
                    {
                        await _orderCustomerProductsServices.AddAsync(new OrderProducts()
                        {
                            OrderId = order.Id,
                            ProductId = product.Id
                        });
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ResponseOrderDto> GetAllOrdersAgainstCustomerAsyc(int customerId)
        {
            var entity = await _orderRepo.GetAsync(x => x.CustomerId.Equals(customerId), a => a.Customer);
            return SetOrderResponseDto(entity);
        }

        public IQueryable<ResponseOrderDto> List()
        {
            try
            {
                var orderCustomers = _orderRepo.GetAll(new List<string> { "OrderProducts.Product" }, a => a.Customer, b => b.OrderProducts);

                // return dto list
                return from c in orderCustomers select SetOrderResponseDto(c);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region private methods

        private static ResponseOrderDto SetOrderResponseDto(Order entity)
        {
            return new()
            {
                Id = entity.Id,
                Customer = new()
                {
                    Id = entity.Customer.Id,
                    Name = entity.Customer?.Name,
                    Address = entity.Customer?.Address,
                },
                Products = entity.OrderProducts
                      .Where(x => x.Product is not null)
                      .Select(y => new ProductDto()
                      {
                          Id = y.Product.Id,
                          Name = y.Product.Name,
                          Description = y.Product.Description,
                          Price = y.Product.Price
                      })
            };
        }

        #endregion
    }
}