using Catalog.Host.Models.Requests.BaseRequests;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Update
{
    public class UpdateProductRequest : BaseProductRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
