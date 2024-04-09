using Order.Host.Data.Entities;

namespace Order.Host.Data.Entities
{
    public class OrderEntity : BaseEntity
    {
        public string UserId { get; set; }
        public decimal TotalPriceItems { get; set; }
        public int AmountProducts { get; set; }
        public List<OrderItemEntity> Items { get; set; }
    }
}
