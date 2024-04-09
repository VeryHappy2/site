using Order.Host.Models.Requests.BaseRequests;
using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests.Update
{
    public class UpdateOrderRequest : BaseOrderRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
