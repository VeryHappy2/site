using System.ComponentModel.DataAnnotations;

namespace MVC.Host.Models.Requests.BaseRequests
{
    public class BaseOrderItemRequest
    {
		[Required]
		[MaxLength(50)]
		public string Name { get; set; }
		[Required]
		[MaxLength(50)]
		public string CreatedAt { get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int Amount { get; set; }
		[Required]
		[Range(0, int.MaxValue)]
		public int ProductId { get; set; }
	}
}
