namespace MVC.ViewModels.BasketViewModels
{
	public class BasketViewModel
	{
		public List<BasketProduct> Data { get; set; }
        public decimal SumPrice { get; set; }
        public BasketViewModel()
        {
            if(Data != null)
            {
                foreach(BasketProduct item in Data) 
                {
                    SumPrice += item.ProductPrice;
                }
            }
            
        }
    }
}
