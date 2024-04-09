namespace MVC.Models.Responses
{
    public class OrderItemResponse
    {
		public int Id { get; set; }
		public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public string CreatedAt { get; set; }
    }
}
