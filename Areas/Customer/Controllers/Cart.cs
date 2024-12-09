using Microsoft.AspNetCore.Mvc;

namespace MVC.Areas.Customer.Controllers
{
    public class Cart : Controller
    {
        // GET: Cart
        [Area("Customer")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
