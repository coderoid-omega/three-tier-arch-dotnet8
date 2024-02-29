using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.UI.Controllers
{
    public class ConcertController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
