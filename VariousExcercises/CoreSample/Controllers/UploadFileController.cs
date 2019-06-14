using CoreSample.ViewModels;
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
