namespace MVC.Host.ViewModels
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; } = null!;
		public string ProductDescription { get; set; } = null!;
		public decimal ProductPrice { get; set; }
		public string PictureUrl { get; set; } = null!;
		public string ProductBrand { get; set; } = null!;
		public string ProductType { get; set; } = null!;
		public int Amount { get; set; }
		public int AvailableStock { get; set; }
	}
}
