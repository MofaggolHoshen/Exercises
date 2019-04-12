using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FiltersSample.Models;
using FiltersSample.Domain;
using FiltersSample.Filters;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersSample.Controllers
{
    [ServiceFilter(typeof(GetModelFromActionFilterAttribute))]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        //[ServiceFilter(typeof(GetModelFromActionFilterAttribute))]
        [HttpGet]
        public IActionResult Me(int id, int vv, bool v)
        {
            var model = new Person
            {
                FirstName = "Mofaggol",
                LastName = "Hoshen",
                Address = new Address
                {
                    City = "Frankfurt am Main",
                    Country = "Germany"
                }
            };

            return View(model);
        }

        public IActionResult Me1(Person result)
        {

            return RedirectToAction("About", "Home");
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
    }
}
