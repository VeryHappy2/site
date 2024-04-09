using System.ComponentModel.DataAnnotations;

namespace Order.Host.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
