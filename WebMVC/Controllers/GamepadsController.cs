using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Services;
using eStore.Domain.Entities;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class GamepadsController : Controller
    {
        private readonly IGoodsService _goodsService;
        private readonly IGamepadService _gamepadService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public GamepadsController(IGoodsService goodsService, IGamepadService gamepadService, IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            _goodsService = goodsService;
            _gamepadService = gamepadService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index([FromQuery] GamepadFilterModel filterModel)
        {
            ViewBag.Manufacturers = await _gamepadService.GetManufacturersAsync();
            ViewBag.Feedbacks = await _gamepadService.GetFeedbacksAsync();
            ViewBag.ConnectionTypes = await _gamepadService.GetConnectionTypesAsync();
            ViewBag.CompatibleDevices = await _gamepadService.GetCompatibleDevicesAsync();
            var gamepads = await _gamepadService.GetPresentByFilterAsync(filterModel);
            var models = _mapper.Map<IEnumerable<GamepadViewModel>>(gamepads);
            await CheckIfInCartAsync(models);

            return View(models);
        }

        public async Task<IActionResult> Gamepad(int id)
        {
            Gamepad gamepad = await _gamepadService.GetByIdAsync(id);
            if (gamepad == null || gamepad.IsDeleted)
            {
                return NotFound();
            }

            var model = _mapper.Map<GamepadViewModel>(gamepad);
            await CheckIfInCartAsync(model);

            return View(model);
        }

        private async Task CheckIfInCartAsync(GoodsViewModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
            }
        }

        private async Task CheckIfInCartAsync(IEnumerable<GoodsViewModel> models)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
                foreach (GoodsViewModel model in models)
                {
                    model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
                }
            }
        }
    }
}