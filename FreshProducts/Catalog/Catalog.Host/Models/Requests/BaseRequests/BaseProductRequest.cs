using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.BaseRequests
{
    public class BaseProductRequest 
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} field must be greater than or equal to 0.")]
        public decimal Price { get; set; }

        [Required]
        public string PictureFileName { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} field must be greater than or equal to 0.")]
        public int CatalogTypeId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} field must be greater than or equal to 0.")]
        public int CatalogBrandId { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The {0} field must be greater than or equal to 0.")]
        public int AvailableStock { get; set; }
    }
}
