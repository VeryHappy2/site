using System.ComponentModel.DataAnnotations;

namespace MVC.Host.Models.Requests.BaseRequests
{
    public class BaseOrderItemRequest
    {
		public string Name { get; set; }
		public string CreatedAt { get; set; }
		public decimal Price { get; set; }
		public int Amount { get; set; }
		public int ProductId { get; set; }
	}
}
