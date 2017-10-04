using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp.Controllers
{
    public class ContactController : Controller
    {
        [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Client)]
        public IActionResult Index()
        {
            return View();
        }
    }
}
