using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
    }
}
