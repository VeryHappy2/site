namespace MVC;

public class AppSettings
{
	public string OrderUrl { get; set; }
	public string OrderItemUrl { get; set; }
	public string OrderOrderUrl { get; set; }
    public string BasketUrl { get; set; }
    public string CatalogUrl { get; set; }
    public int SessionCookieLifetimeMinutes { get; set; }
    public string CallBackUrl { get; set; }
    public string IdentityUrl { get; set; }
}
