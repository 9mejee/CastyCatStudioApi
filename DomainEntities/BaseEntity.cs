using System.ComponentModel.DataAnnotations;

namespace DomainEntities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}