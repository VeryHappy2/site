using Order.Host.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace Order.Host.Models.Requests.BaseRequests
{
    public class BaseOrderRequest
    {
		[Required]
		public decimal TotalPriceItems { get; set; }
		[Required]
		public int AmountProducts { get; set; }
		public List<BaseOrderItemRequest> Items { get; set; }
	}
}
