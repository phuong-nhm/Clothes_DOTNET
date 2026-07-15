using CLothes_DOTNET.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CLothes_DOTNET.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public new IActionResult User()
        {
            return View();
        }
        public IActionResult Card()
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
