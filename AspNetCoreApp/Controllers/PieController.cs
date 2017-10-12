using AspNetCoreApp.Helpers;
using AspNetCoreApp.Models;
using AspNetCoreApp.Models.Repositories;
using AspNetCoreApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text.Encodings.Web;

namespace AspNetCoreApp.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ILogger<PieController> _logger;
        private readonly IPieReviewRepository _pieReviewRepository;
        private readonly HtmlEncoder _htmlEncoder;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository, ILogger<PieController> logger,
            IPieReviewRepository pieReviewRepository, HtmlEncoder htmlEncoder)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
            _logger = logger;
            _pieReviewRepository = pieReviewRepository;
            _htmlEncoder = htmlEncoder;
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

        [Route("[controller]/Details/{id}")]
        [HttpPost]
        public IActionResult Details(int id, string review)
        {
            var pie = _pieRepository.GetPieById(id);
            if (pie == null)
            {
                _logger.LogWarning(LogEventIds.GetPieIdNotFound, new Exception("Pie not found"), "Pie with id {0} not found", id);
                return NotFound();
            }

            string encodedReview = _htmlEncoder.Encode(review);

            _pieReviewRepository.AddPieReview(new PieReview() { Pie = pie, Review = encodedReview });

            return View(new PieDetailViewModel() { Pie = pie });
        }
    }
}
