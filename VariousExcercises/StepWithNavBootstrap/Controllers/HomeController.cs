using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StepWithNavBootstrap.Models;

namespace StepWithNavBootstrap.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var l = MyMethod();

            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
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

        public int MyMethod()
        {
            var k = 0;
            for (int i = 0; i < 1000000000; i++)
            {
                k *= i;
            }

            return k;
        }
    }
}
