using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Application.Interfaces;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IEmailService _emailService;
        private readonly IGoodsService _goodsService;
        private readonly IMapper _mapper;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(ICustomerService customerService, IGoodsService goodsService,
            IMapper mapper, IEmailService emailService,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _customerService = customerService;
            _goodsService = goodsService;
            _mapper = mapper;
            _emailService = emailService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var customer = await _customerService.GetCustomerByIdAsync(user.CustomerId);
            var model = _mapper.Map<CustomerViewModel>(customer);
            foreach (var goods in customer.ShoppingCart.Goods)
                if (goods is Keyboard keyboard)
                    model.GoodsInCart.Add(_mapper.Map<KeyboardViewModel>(keyboard));
                else if (goods is Mouse mouse)
                    model.GoodsInCart.Add(_mapper.Map<MouseViewModel>(mouse));
                else if (goods is Mousepad mousepad)
                    model.GoodsInCart.Add(_mapper.Map<MousepadViewModel>(mousepad));
                else if (goods is Gamepad gamepad) 
                    model.GoodsInCart.Add(_mapper.Map<GamepadViewModel>(gamepad));

            foreach (var goods in model.GoodsInCart)
                goods.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, goods.Id);

            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CustomerViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var customer = _mapper.Map<Customer>(model);
                    await _customerService.UpdateCustomerInfoAsync(customer);
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            
            return RedirectToAction("Index", "Account");
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            var model = new RegisterViewModel();
            return await Task.FromResult(View(model));
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            try
            {
                var identityUser = new ApplicationUser
                {
                    Email = model.Email,
                    UserName = model.Email
                };
                var customer = new Customer
                {
                    Email = model.Email,
                    IdentityId = identityUser.Id,
                    IsDeleted = false,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    Country = model.Country,
                    City = model.City,
                    Address = model.Address,
                    PostalCode = model.PostalCode
                };
                await _customerService.AddCustomerAsync(customer);
                identityUser.CustomerId = customer.Id;
                var registerResult = await _userManager.CreateAsync(identityUser, model.Password);
                if (registerResult.Succeeded) 
                    await _signInManager.SignInAsync(identityUser, true);
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                return View(model);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var model = new LoginViewModel();
            return await Task.FromResult(View(model));
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                if (string.IsNullOrWhiteSpace(model.ReturnUrl)) 
                    return RedirectToAction("Index", "Home");

                return Redirect(model.ReturnUrl);
            }

            ModelState.AddModelError("", "Email and/or password is incorrect.");
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddGoodsToCart(int goodsId, string returnUrl = null)
        {
            var identityUser = await _userManager.GetUserAsync(HttpContext.User);
            var customer = await _customerService.GetCustomerByIdAsync(identityUser.CustomerId);
            await _customerService.AddGoodsToCartAsync(customer.Id, goodsId);
            if (string.IsNullOrWhiteSpace(returnUrl)) 
                return RedirectToAction("Index", "Goods");

            return Redirect(returnUrl);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveGoodsFromCart(int goodsId, string returnUrl = null)
        {
            var identityUser = await _userManager.GetUserAsync(HttpContext.User);
            var customer = await _customerService.GetCustomerByIdAsync(identityUser.CustomerId);
            await _customerService.RemoveGoodsFromCartAsync(customer.Id, goodsId);
            if (string.IsNullOrWhiteSpace(returnUrl)) 
                return RedirectToAction("Index", "Goods");

            return Redirect(returnUrl);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            var model = new ChangePasswordViewModel();
            return await Task.FromResult(View(model));
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                if (!await _userManager.CheckPasswordAsync(user, model.CurrentPassword))
                {
                    ModelState.AddModelError("", "The current password is not correct.");
                    return View(model);
                }

                await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
                await _emailService.SendChangePasswordEmailAsync(user.Email);
                return RedirectToAction("Index", "Account");
            }

            return View(model);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeactivateAccount()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var customer = await _customerService.GetCustomerByIdAsync(user.CustomerId);
            await _customerService.DeactivateAccountAsync(customer.Id);
            await _userManager.DeleteAsync(user);
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}