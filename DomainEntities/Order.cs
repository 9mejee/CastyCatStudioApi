using System.ComponentModel.DataAnnotations.Schema;

namespace DomainEntities
{
    public class Order : BaseEntity
    {
        public Order()
        {
            OrderProducts = new HashSet<OrderProducts>();
        }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderProducts> OrderProducts { get; set; }
    }
}