using System.ComponentModel.DataAnnotations;

namespace MVC.Host.Models.Requests.BaseRequests
{
    public class BaseOrderRequest
    {
		[Required]
		public decimal TotalPriceItems { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int AmountProducts { get; set; }
		public List<BaseOrderItemRequest> Items { get; set; }
	}
}
