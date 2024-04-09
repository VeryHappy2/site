using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.BaseRequests
{
    public class BaseTypeRequest
    {
        [Required]
        [MaxLength(65)]
        public string Type { get; set; }
    }
}
