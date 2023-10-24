using System.ComponentModel.DataAnnotations;

namespace DTO.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Address { get; set; }
    }
}