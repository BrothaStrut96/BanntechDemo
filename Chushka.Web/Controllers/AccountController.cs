using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Chushka.Data;
using Chushka.Data.Models;
using Chushka.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Chushka.Web.Controllers
{
    public class AccountController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<HomeController> _logger;

        public AccountController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
            Response.Redirect("/Account/Login");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginFormModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(apu => apu.UserName.Equals(model.Username));
                if (user != null)
                {
                    //Go To User Homepage
                    var session = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    if (user != null)
                        Response.Redirect("/");
                        
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterAsync(RegisterFormModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.Password.Equals(model.ConfirmPassword))
                {
                    var check = await _context.Users.FirstOrDefaultAsync(apu => apu.Email.Equals(model.Email));
                    if (check != null)
                    {
                        ModelState.AddModelError("EMAILINUSE", "Email is already in use by another account.");
                        return View(model);
                    }

                    //Create New User Account
                    var newUser = new ApplicationUser()
                    {
                        UserName = model.Username,
                        Email = model.Email,
                        FullName = model.FullName
                    };

                    var result = await _userManager.CreateAsync(newUser, model.Password);
                    if (result.Succeeded)
                    {

                        //Check if is Genisis User
                        var usercnt = _context.Users.Count();
                        if (usercnt == 1)
                        {
                            var user = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.UserName.Equals(newUser.UserName));
                            var roleResult = await _userManager.AddToRoleAsync(user, "Admin");
                            if (roleResult.Succeeded)
                            {
                                //Go To User Homepage
                                var signin = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                                if (signin.Succeeded)
                                {
                                    Response.Redirect("/");
                                }
                                
                            }
                        }
                        else
                        {
                            var user = await _context.Set<ApplicationUser>().FirstOrDefaultAsync(user => user.UserName.Equals(newUser.UserName));
                            var roleResult = await _userManager.AddToRoleAsync(user, "User");
                            if (roleResult.Succeeded)
                            {
                                //Go To User Homepage
                                var signin = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
                                if (signin.Succeeded)
                                {
                                    Response.Redirect("/");
                                }
                            }
                        }
                        return View(model);
                    }

                    ModelState.AddModelError("CREATEERROR", "Unable to create account.");
                    return View(model);

                }
                ModelState.AddModelError("CONPASSERROR", "Passwords do not match.");
                return View(model);
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied(string returnUrl = null)
        {
            return RedirectToAction("Index", "Home");
        }
    }
}