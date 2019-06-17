using CoreSample.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSample.Controllers
{
    public class UploadFileController : Controller
    {
        [HttpGet]
        public IActionResult Photo()
        {
            var brows = Request.Headers["User-Agent"].ToString();

            return View();
        }

        [HttpPost]
        public IActionResult Photo(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
            }
            return View(userViewModel);
        }
    }
}
