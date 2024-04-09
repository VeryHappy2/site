using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.BaseRequests
{
    public class BaseBrandRequest
    {
        [Required]
        [MaxLength(65)]
        public string Brand {  get; set; }
    }
}
