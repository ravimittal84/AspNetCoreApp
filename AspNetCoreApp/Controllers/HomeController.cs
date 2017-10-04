using AspNetCoreApp.Models.Repositories;
using AspNetCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPieRepository _pieRepository;

        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }

        public ViewResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                PiesOfTheWeek = _pieRepository.PiesOftheWeek
            };

            return View(homeViewModel);
        }
    }
}
