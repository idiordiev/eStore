using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Application.FilterModels;
using eStore.Application.Interfaces.Services;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGamepadService _gamepadService;
        private readonly IGoodsService _goodsService;
        private readonly IKeyboardService _keyboardService;
        private readonly IMapper _mapper;
        private readonly IMousepadService _mousepadService;
        private readonly IMouseService _mouseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GoodsController(IGoodsService goodsService, IGamepadService gamepadService,
            IKeyboardService keyboardService, IMouseService mouseService,
            IMousepadService mousepadService, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _goodsService = goodsService;
            _gamepadService = gamepadService;
            _keyboardService = keyboardService;
            _mouseService = mouseService;
            _mousepadService = mousepadService;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }

        [HttpGet("keyboards")]
        public async Task<IActionResult> Keyboards([FromQuery] KeyboardFilterModel filterModel)
        {
            var keyboards = await _keyboardService.GetPresentByFilterAsync(filterModel);
            var models = _mapper.Map<IEnumerable<KeyboardViewModel>>(keyboards);
            ViewBag.Manufacturers = await _keyboardService.GetManufacturersAsync();
            ViewBag.Sizes = await _keyboardService.GetSizesAsync();
            ViewBag.Types = await _keyboardService.GetTypesAsync();
            ViewBag.Switches = await _keyboardService.GetSwitchesAsync();
            ViewBag.ConnectionTypes = await _keyboardService.GetConnectionTypesAsync();
            ViewBag.Backlights = await _keyboardService.GetBacklightsAsync();
            ViewBag.KeyRollovers = await _keyboardService.GetKeyRolloverAsync();
            await CheckIfInCartAsync(models);

            return View(models);
        }

        private async Task CheckIfInCartAsync(GoodsViewModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
            }
        }

        private async Task CheckIfInCartAsync(IEnumerable<GoodsViewModel> models)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                foreach (var model in models)
                    model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
            }
        }

        [HttpGet("keyboards/{id}")]
        public async Task<IActionResult> Keyboard(int id)
        {
            var keyboard = await _keyboardService.GetByIdAsync(id);
            if (keyboard == null || keyboard.IsDeleted) 
                return NotFound();

            var model = _mapper.Map<KeyboardViewModel>(keyboard);
            await CheckIfInCartAsync(model);

            return View(model);
        }

        [HttpGet("mouses")]
        public async Task<IActionResult> Mouses([FromQuery] MouseFilterModel filterModel)
        {
            ViewBag.Manufacturers = await _mouseService.GetManufacturersAsync();
            ViewBag.Backlights = await _mouseService.GetBacklightsAsync();
            ViewBag.ConnectionTypes = await _mouseService.GetConnectionTypesAsync();
            var mouses = await _mouseService.GetPresentByFilterAsync(filterModel);
            var models = _mapper.Map<IEnumerable<MouseViewModel>>(mouses);
            await CheckIfInCartAsync(models);

            return View(models);
        }

        [HttpGet("mouses/{id}")]
        public async Task<IActionResult> Mouse(int id)
        {
            var mouse = await _mouseService.GetByIdAsync(id);
            if (mouse == null || mouse.IsDeleted) 
                return NotFound();

            var model = _mapper.Map<MouseViewModel>(mouse);
            await CheckIfInCartAsync(model);

            return View(model);
        }

        [HttpGet("mousepads")]
        public async Task<IActionResult> Mousepads([FromQuery] MousepadFilterModel filterModel)
        {
            ViewBag.Manufacturers = await _mousepadService.GetManufacturersAsync();
            ViewBag.TopMaterials = await _mousepadService.GetTopMaterialsAsync();
            ViewBag.BottomMaterials = await _mousepadService.GetBottomMaterialsAsync();
            ViewBag.Backlights = await _mousepadService.GetBacklightsAsync();
            var mousepads = await _mousepadService.GetPresentByFilterAsync(filterModel);
            var models = _mapper.Map<IEnumerable<MousepadViewModel>>(mousepads);
            await CheckIfInCartAsync(models);

            return View(models);
        }

        [HttpGet("mousepads/{id}")]
        public async Task<IActionResult> Mousepad(int id)
        {
            var mousepad = await _mousepadService.GetByIdAsync(id);
            if (mousepad == null || mousepad.IsDeleted) 
                return NotFound();

            var model = _mapper.Map<MousepadViewModel>(mousepad);
            await CheckIfInCartAsync(model);

            return View(model);
        }

        [HttpGet("gamepads")]
        public async Task<IActionResult> Gamepads([FromQuery] GamepadFilterModel filterModel)
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

        [HttpGet("gamepads/{id}")]
        public async Task<IActionResult> Gamepad(int id)
        {
            var gamepad = await _gamepadService.GetByIdAsync(id);
            if (gamepad == null || gamepad.IsDeleted) 
                return NotFound();

            var model = _mapper.Map<GamepadViewModel>(gamepad);
            await CheckIfInCartAsync(model);

            return View(model);
        }
    }
}