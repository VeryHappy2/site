using Order.Host.Data.Entities;

namespace Order.Host.Models.Dtos;

public class OrderDto : BaseDto
{
	public string UserId { get; set; }
	public decimal TotalPriceItems { get; set; }
	public int AmountProducts { get; set; }
	public List<OrderItemDto> Items { get; set; }
}
