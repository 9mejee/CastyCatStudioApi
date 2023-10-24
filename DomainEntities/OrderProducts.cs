using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
    public class OrderProducts : BaseEntity
    {
        [ForeignKey("Order")]
        public int OrderId { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
