using Clothes_DOTNET.Service;
using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Diagnostics;
using System.Text.Json;

namespace CLothes_DOTNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly List_Category_Service _listcategoryService;
        private readonly CategoryService _categoryService;
        private readonly CategoryClaimService _categoryclaimService;
        public HomeController(ILogger<HomeController> logger, List_Category_Service listcategoryService, CategoryService categoryService, CategoryClaimService categoryclaimService)
        {
            _logger = logger;
            _listcategoryService = listcategoryService;
            _categoryService = categoryService;
            _categoryclaimService = categoryclaimService;
        }

        public IActionResult Index()

        {
            var products = ProductData.GetProducts();
            _listcategoryService.gender = _categoryService.GetGenderCategorys();
            return View(products);
        }
        [HttpGet]
        public IActionResult getcategory()
        {

            var categoris= _categoryService.GetCategorys();
            var categoryclaims = _categoryclaimService.getcategoryclaims();
            return Json(new { categoris, categoryclaims });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
