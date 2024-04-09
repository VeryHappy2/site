using Order.Host.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests.BaseRequests
{
    public class BaseOrderItemRequest
    {
		[Required]
		[MaxLength(65)]
		public string Name { get; set; }
		[Required]
		public string CreatedAt { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public int Amount { get; set; }
		[Required]
		public int ProductId { get; set; }
	}
}
