using Catalog.Host.Models.BaseRequests;
using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.Update
{
    public class UpdateTypeRequest : BaseTypeRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
