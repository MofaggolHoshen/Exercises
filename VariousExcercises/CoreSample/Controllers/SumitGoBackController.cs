using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FiltersSample.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FiltersSample.Controllers
{
    public class SumitGoBackController : Controller
    {
        public IActionResult Index()
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

        public IActionResult SaveNgoBack(Person arg)
        {
            var j = arg;

            if (Request.Headers.Any(i => i.Key == "Referer"))
                return Redirect(Request.Headers["Referer"].ToString());

            return View();
        }

        private void HandleNewJImage(object sender, FileSystemEventArgs e)
        {
           // TODO: This event can not return your view.
        }

        public IActionResult PartialViewImage()
        {

            return PartialView("Your-View-Name");
        }
    }
}