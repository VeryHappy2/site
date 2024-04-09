using Order.Host.Data.Entities;
using Order.Host.Models.Dtos;

namespace Order.Host.Models.BaseResponses
{
    public class BaseOrderResponse
    {
        public int Id { get; set; }
		public string UserId { get; set; }
		public decimal TotalPriceItems { get; set; }
		public int AmountProducts { get; set; }
		public List<OrderItemDto> Items { get; set; }
	}
}
