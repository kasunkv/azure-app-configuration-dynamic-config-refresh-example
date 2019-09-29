using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DynamicRefreshExample.Models;
using Microsoft.Extensions.Options;

namespace DynamicRefreshExample.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppSettings _appSettings;

        public HomeController(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Title = _appSettings.HomePage.Title,
                Description = _appSettings.HomePage.Description
            };

            return View(vm);
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
