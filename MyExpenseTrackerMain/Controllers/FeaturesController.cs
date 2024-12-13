using Microsoft.AspNetCore.Mvc;

namespace MyExpenseTracker.Web.Controllers
{
    public class FeaturesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

