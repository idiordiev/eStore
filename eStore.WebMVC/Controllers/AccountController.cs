using System;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Services;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public AccountController(ICustomerService customerService, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _customerService = customerService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var identityUser = new IdentityUser()
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var registerResult = await _userManager.CreateAsync(identityUser, model.Password);
                if (registerResult.Succeeded)
                {
                    var customer = new Customer()
                    {
                        Email = model.Email,
                        IdentityId = identityUser.Id,
                        IsDeleted = false
                    };
                    await _customerService.AddCustomerAsync(customer);
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return Redirect(model.ReturnUrl);
            }
            
            ModelState.AddModelError("", "Email and/or password is incorrect.");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}