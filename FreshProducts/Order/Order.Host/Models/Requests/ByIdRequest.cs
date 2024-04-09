using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests
{
    public class ByIdRequest
    {
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "The field must be greater than or equal to 0.")]
        public int Id { get; set; }
    }
}
