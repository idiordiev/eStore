using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.DomainServices;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.DTO;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(IOrderService orderService, ICustomerService customerService,
            UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _orderService = orderService;
            _customerService = customerService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var orders = await _orderService.GetOrdersByCustomerIdAsync(user.CustomerId);
            var model = _mapper.Map<IEnumerable<OrderViewModel>>(orders);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> New()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var customer = await _customerService.GetCustomerByIdAsync(user.CustomerId);
            var model = new OrderViewModel
            {
                ShippingCountry = customer.Country,
                ShippingCity = customer.City,
                ShippingAddress = customer.Address,
                ShippingPostalCode = customer.PostalCode,
                OrderItems = new List<OrderItemViewModel>()
            };
            var goods = customer.ShoppingCart.Goods.Select(g => g.Goods);
            foreach (var good in goods)
            {
                if (good is Keyboard keyboard)
                    model.OrderItems.Add(new OrderItemViewModel
                    {
                        Goods = _mapper.Map<KeyboardViewModel>(keyboard),
                        Quantity = 1,
                        UnitPrice = keyboard.Price,
                        GoodsId = keyboard.Id
                    });
                else if (good is Mouse mouse)
                    model.OrderItems.Add(new OrderItemViewModel
                    {
                        Goods = _mapper.Map<MouseViewModel>(mouse),
                        Quantity = 1,
                        UnitPrice = mouse.Price,
                        GoodsId = mouse.Id
                    });
                else if (good is Mousepad mousepad)
                    model.OrderItems.Add(new OrderItemViewModel
                    {
                        Goods = _mapper.Map<MousepadViewModel>(mousepad),
                        Quantity = 1,
                        UnitPrice = mousepad.Price,
                        GoodsId = mousepad.Id
                    });
                else if (good is Gamepad gamepad)
                    model.OrderItems.Add(new OrderItemViewModel
                    {
                        Goods = _mapper.Map<GamepadViewModel>(gamepad),
                        Quantity = 1,
                        UnitPrice = gamepad.Price,
                        GoodsId = gamepad.Id
                    });
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New(OrderViewModel model)
        {
            if (!ModelState.IsValid) 
                return View(model);

            var orderItems = _mapper.Map<IEnumerable<OrderItemDto>>(model.OrderItems);
            var shippingAddress = _mapper.Map<OrderAddressDto>(model);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            await _orderService.CreateOrderAsync(user.CustomerId, orderItems, shippingAddress);
            await _customerService.ClearCustomerCartAsync(user.CustomerId);

            TempData["IsSuccess"] = true;
            return RedirectToAction("Success", "Order");
        }

        [HttpGet]
        public async Task<IActionResult> Success()
        {
            if (TempData.ContainsKey("IsSuccess") && (bool)TempData["IsSuccess"]) 
                return await Task.Run(View);

            return Forbid();
        }
    }
}