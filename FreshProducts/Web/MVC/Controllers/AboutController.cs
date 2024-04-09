namespace MVC.Controllers
{
    public class AboutController : Controller
    {
        public AboutController()
        {
        }

        public async Task<IActionResult> Index()
        {

            return View();
        }
    }
}
