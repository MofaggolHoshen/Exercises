using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EShop.Web.Models;
using EShop.Services;
using EShop.Domain;

namespace EShop.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EShopService _eShopService;

        public HomeController(ILogger<HomeController> logger, EShopService shopService)
        {
            _logger = logger;
            _eShopService = shopService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Products()
        {
            var products = _eShopService.InitShop().Products;

            return View(products);
        }

        public IActionResult Product(int id = 0)
        {
            Product product = new Product();

            if (id > 0)
            {
                product = _eShopService.InitShop().Product(id);
            }

            return View(product);
        }

        public IActionResult SaveProduct(Product product)
        {

            if (product.Id > 0)
                _eShopService.InitShop().Product(product.Id).Set(product);
            else
                product = _eShopService.InitShop().AddProduct(product);

            _eShopService.Persists();

            return RedirectToAction("Product", new { id = product.Id });
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
