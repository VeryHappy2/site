namespace MVC.Models.Requests.BaseRequests
{
    public class BaseOrderRequest
    {
        public string UserId { get; set; }
        public decimal TotalPriceItems { get; set; }
        public int AmountProducts { get; set; }
        public List<BaseOrderItemRequest> Items { get; set; }
    }
}
