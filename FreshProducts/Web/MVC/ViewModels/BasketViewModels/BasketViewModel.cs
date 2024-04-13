namespace MVC.ViewModels.BasketViewModels
{
	public class BasketViewModel
	{
		public List<BasketProduct> Data { get; set; }
        public decimal SumPrice { get; set; }
        public int Amount { get; set; }
    }
}
