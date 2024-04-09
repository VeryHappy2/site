namespace MVC.Models.Responses
{
    public class OrderResponse
	{
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPriceItems { get; set; }
        public int AmountProducts { get; set; }
        public List<OrderItemResponse> Items { get; set; }
    }
}
