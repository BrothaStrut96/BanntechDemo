using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Chushka.Web.Models;
using Chushka.Data;
using Microsoft.EntityFrameworkCore;
using Chushka.Shared.Models;

namespace Chushka.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var model = new AuthenticatedUserHomePageViewModel();

            var products = await _context.
                Products
                .Select(prod=> new ProductDtoModel()
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Description = prod.Description,
                    Price = prod.Price,
                    Type = prod.Type
                })
                .ToListAsync();

            model.Products = products?.ToList();

            return View(model);
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
