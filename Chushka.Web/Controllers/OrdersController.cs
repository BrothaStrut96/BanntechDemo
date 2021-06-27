using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chushka.Data;
using Chushka.Data.Models;
using Chushka.Shared.Models;
using Chushka.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Chushka.Web.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public OrdersController(ILogger<HomeController> logger, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var orders = new OrderViewModel()
            {
                Orders = await _context
                .Orders
                .Include(order=>order.Product)
                .Include(order=>order.User)
                .Select(order => new OrderDtoModel()
                {
                    Id = order.Id,
                    Product = new ProductDtoModel()
                    {
                        Id = order.Product.Id,
                        Name = order.Product.Name,
                        Description = order.Product.Description,
                        Price = order.Product.Price,
                        Type = order.Product.Type
                    },
                    Customer = order.User.UserName,
                    OrderCreated = order.OrderCreated,
                    UserId = order.UserId
                })
                .ToListAsync()
            };
            
            return View(orders);
        }

        [HttpPost]
        [Authorize(Roles = "User,Admin")]
        public async Task<IActionResult> Create([FromBody] CreateOrderViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            model.UserId = user.Id;

            if (ModelState.IsValid)
            {
                var newOrder = new OrderDtoModel()
                {
                    ProductId = model.ProductId,
                    UserId = model.UserId,
                    OrderCreated = DateTime.Now
                };

                await _context.Orders.AddAsync(model.DtoToEntitiy(newOrder));

                await _context.SaveChangesAsync();

                var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                if(isAdmin)
                {
                    return Json(new { message = "go2order" });
                }
                return Json(new { message = "success" });
            }
            return Json(new { message = "failed" });
        }
    }
}