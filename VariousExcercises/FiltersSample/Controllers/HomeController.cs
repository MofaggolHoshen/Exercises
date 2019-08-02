using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiltersSample.Models;
using FiltersSample.Services;
using FiltersSample.Filters;
using System.Globalization;

namespace FiltersSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService userService;

        public HomeController(IUserService user)
        {
            userService = user;
        }

        //[TypeFilterSample]
        //[ServiceFilter(typeof(ServiceFilterSample))]
        public IActionResult Index()
        {
            var r = 20;
            return View();
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

        [TypeFilter(typeof(LogConstantFilter),
            Arguments = new object[] { "Method 'Hi' called" })]
        public IActionResult Hi(string name)
        {
            return Content($"Hi {name}");
        }

        [Route("{culture}/[controller]/[action]")]
        [MiddlewareFilter(typeof(LocalizationPipeline))]
        public IActionResult CultureFromRouteData()
        {
            return Content($"CurrentCulture:{CultureInfo.CurrentCulture.Name},"
                + $"CurrentUICulture:{CultureInfo.CurrentUICulture.Name}");
        }
    }
}
