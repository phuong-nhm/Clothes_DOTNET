using Microsoft.AspNetCore.Mvc;

namespace Clothes_DOTNET.Controllers
{
    public class AllProductController : Controller
    {
        public IActionResult AllProduct()
        {
            return View();
        }
    }
}
