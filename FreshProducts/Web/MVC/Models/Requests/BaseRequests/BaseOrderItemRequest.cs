namespace MVC.Models.Requests.BaseRequests
{
    public class BaseOrderItemRequest
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string CreatedAt { get; set; }
		public int OrderId { get; set; }
	}
}
