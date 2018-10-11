using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [AllowAnonymous]
    //[Authorize]
    public class HomeController : Controller
    {
        private readonly DataContext _context;
        public HomeController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var Administrator = User.IsInRole("Administrator");
            var FrontOfficer = User.IsInRole("FrontOfficer");

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

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [AllowAnonymous]
        public IActionResult AuthError(string message)
        {
            
            ViewBag.ErrorMessage = message;

            // Removing Session
           // HttpContext.Session.Clear();

            // Removing Cookies
            CookieOptions option = new CookieOptions();
            if (Request.Cookies[".AspNetCore.Session"] != null)
            {
                option.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Append(".AspNetCore.Session", "", option);
            }

            if (Request.Cookies["AuthenticationToken"] != null)
            {
                option.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Append("AuthenticationToken", "", option);
            }

            return View();
        }
    }
}
