using Microsoft.AspNetCore.Mvc;

namespace ConcertBooking.UI.Controllers
{
    public class ArtistController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
