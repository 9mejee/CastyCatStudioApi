using DTO.Customer;
using DTO.Product;

namespace DTO.OrderRequest
{
    public class ResponseOrderDto
    {
        public int Id { get; set; }
        public CustomerDto Customer { get; set; }
        public IEnumerable<ProductDto> Products { get; set; }
    }
}
