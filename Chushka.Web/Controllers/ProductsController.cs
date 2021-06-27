using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chushka.Data;
using Chushka.Data.Models;
using Chushka.Shared.Models;
using Chushka.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chushka.Web.Controllers
{
    public class ProductsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public ProductsController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id.HasValue)
            {
                var product = await _context.Products.FirstOrDefaultAsync(product => product.Id.Equals(id.Value));
                if(product!=null)
                {
                    var model = new ProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Type = product.Type
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new CreateProductViewModel());
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id.HasValue)
            {
                var product = await _context.Products.FirstOrDefaultAsync(product => product.Id.Equals(id.Value));
                if (product != null)
                {
                    var model = new EditProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Type = product.Type
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id.HasValue)
            {
                var product = await _context.Products.FirstOrDefaultAsync(product => product.Id.Equals(id.Value));
                if (product != null)
                {
                    var model = new DeleteProductViewModel()
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        Price = product.Price,
                        Type = product.Type
                    };
                    return View(model);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if(ModelState.IsValid)
            {
               await _context
                    .Products
                    .AddAsync(new ProductModel()
                    {
                        Name = model.Name,
                        Description = model.Description,
                        Price = model.Price,
                        Type = model.Type
                    });
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(model.Id));
                if (exists != null)
                {
                    exists.Name = model.Name;
                    exists.Description = model.Description;
                    exists.Price = model.Price;
                    exists.Type = model.Type;
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(DeleteProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Products.FirstOrDefaultAsync(prod => prod.Id.Equals(model.Id));
                if (exists != null)
                {
                    //Check to see if product is present in an order, will cause FK Exception
                    var getOrders = await _context.Orders.Where(order => order.ProductId.Equals(exists.Id)).ToListAsync();
                    if (getOrders.Count > 0)
                        _context.Orders.RemoveRange(getOrders);
                    _context.Products.Remove(exists);
                }
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}