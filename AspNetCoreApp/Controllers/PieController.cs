using AspNetCoreApp.Models.Repositories;
using AspNetCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AspNetCoreApp.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        public ViewResult List(string category)
        {
            var model = new PiesListViewModel();

            if (string.IsNullOrEmpty(category))
            {
                model.Pies = _pieRepository.Pies.OrderBy(p => p.PieId);
                model.CurrentCategory = "All pies";
            }
            else
            {
                model.Pies = _pieRepository.Pies.Where(p => p.Category.CategoryName == category)
                   .OrderBy(p => p.PieId);
                model.CurrentCategory = category;
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
                return NotFound();

            return View(pie);
        }
    }
}
