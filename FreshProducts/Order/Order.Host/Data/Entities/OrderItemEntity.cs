using Order.Host.Data.Entities;

namespace Order.Host.Data.Entities
{
    public class OrderItemEntity : BaseEntity
    {
        public string Name { get; set; }
        public string CreatedAt { get; set; }
        public decimal Price { get; set; }
		public int Amount { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
	}
}
