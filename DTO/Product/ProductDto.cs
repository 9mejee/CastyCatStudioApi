using System.ComponentModel.DataAnnotations;

namespace DTO.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}