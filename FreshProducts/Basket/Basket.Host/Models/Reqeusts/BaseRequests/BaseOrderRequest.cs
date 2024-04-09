namespace MVC.Host.Models.Requests.BaseRequests
{
    public class BaseOrderRequest
    {
		public decimal TotalPriceItems { get; set; }
		public int AmountProducts { get; set; }
		public List<BaseOrderItemRequest> Items { get; set; }
	}
}
