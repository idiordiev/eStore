using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGoodsService _goodsService;
        private readonly IGamepadService _gamepadService;
        private readonly IKeyboardService _keyboardService;
        private readonly IMouseService _mouseService;
        private readonly IMousepadService _mousepadService;
        private readonly IMapper _mapper;
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("keyboards")]
        public async Task<IActionResult> Keyboards([FromQuery] ICollection<int> manufacturerIds, 
            [FromQuery] ICollection<int> typeIds, 
            [FromQuery] ICollection<int?> switchIds, 
            [FromQuery] ICollection<int> sizeIds,
            [FromQuery] ICollection<int> connectionTypeIds,
            [FromQuery] ICollection<int> backlightIds,
            [FromQuery] ICollection<int> keyRolloverIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var filter = new KeyboardFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                KeyboardTypeIds = typeIds,
                SwitchIds = switchIds,
                KeyboardSizeIds = sizeIds,
                ConnectionTypeIds = connectionTypeIds,
                BacklightIds = backlightIds,
                KeyRolloverIds = keyRolloverIds,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };
            var keyboards = await _keyboardService.GetPresentByFilterAsync(filter);
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
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
            }
        }

        private async Task CheckIfInCartAsync(IEnumerable<GoodsViewModel> models)
        {
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                foreach (var model in models)
                {
                    model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
                }
            }
        }

        [HttpGet("keyboards/{id}")]
        public async Task<IActionResult> Keyboard(int id)
        {
            var keyboard = await _keyboardService.GetByIdAsync(id);
            if (keyboard == null)
                return NotFound();
            
            var model = _mapper.Map<KeyboardViewModel>(keyboard);
            await CheckIfInCartAsync(model);
            
            return View(model);
        }

        [HttpGet("mouses")]
        public async Task<IActionResult> Mouses([FromQuery] ICollection<int> manufacturerIds,
            [FromQuery] ICollection<int> connectionTypeIds,
            [FromQuery] ICollection<int> backlightIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? minWeight,
            [FromQuery] int? maxWeight)
        {
            var filter = new MouseFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                ConnectionTypeIds = connectionTypeIds,
                BacklightIds = backlightIds,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                MaxWeight = maxWeight,
                MinWeight = minWeight
            };
            ViewBag.Manufacturers = await _mouseService.GetManufacturersAsync();
            ViewBag.Backlights = await _mouseService.GetBacklightsAsync();
            ViewBag.ConnectionTypes = await _mouseService.GetConnectionTypesAsync();
            var mouses = await _mouseService.GetPresentByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<MouseViewModel>>(mouses);
            await CheckIfInCartAsync(models);
            
            return View(models);
        }
        
        [HttpGet("mouses/{id}")]
        public async Task<IActionResult> Mouse(int id)
        {
            var mouse = await _mouseService.GetByIdAsync(id);
            if (mouse == null)
                return NotFound();
            
            var model = _mapper.Map<MouseViewModel>(mouse);
            await CheckIfInCartAsync(model);
            
            return View(model);
        }
        
        [HttpGet("mousepads")]
        public async Task<IActionResult> Mousepads([FromQuery] ICollection<int> manufacturerIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] ICollection<int> topMaterialIds,
            [FromQuery] ICollection<int> bottomMaterialIds,
            [FromQuery] ICollection<bool> isStitchedValues,
            [FromQuery] ICollection<int> backlightIds)
        {
            var filter = new MousepadFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                TopMaterialIds = topMaterialIds,
                BottomMaterialIds = bottomMaterialIds,
                IsStitchedValues = isStitchedValues,
                BacklightIds = backlightIds
            };
            ViewBag.Manufacturers = await _mousepadService.GetManufacturersAsync();
            ViewBag.TopMaterials = await _mousepadService.GetTopMaterialsAsync();
            ViewBag.BottomMaterials = await _mousepadService.GetBottomMaterialsAsync();
            ViewBag.Backlights = await _mousepadService.GetBacklightsAsync();
            var mousepads = await _mousepadService.GetPresentByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<MousepadViewModel>>(mousepads);
            await CheckIfInCartAsync(models);
            
            return View(models);
        }
        
        [HttpGet("mousepads/{id}")]
        public async Task<IActionResult> Mousepad(int id)
        {
            var mousepad = await _mousepadService.GetByIdAsync(id);
            if (mousepad == null)
                return NotFound();
            
            var model = _mapper.Map<MousepadViewModel>(mousepad);
            await CheckIfInCartAsync(model);
            
            return View(model);
        }
        
        [HttpGet("gamepads")]
        public async Task<IActionResult> Gamepads([FromQuery] ICollection<int> manufacturerIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] ICollection<int> connectionTypeIds,
            [FromQuery] ICollection<int> feedbackIds,
            [FromQuery] ICollection<int> compatibleDevicesIds)
        {
            var filter = new GamepadFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                ConnectionTypeIds = connectionTypeIds,
                FeedbackIds = feedbackIds, 
                CompatibleDevicesIds = compatibleDevicesIds
            };
            ViewBag.Manufacturers = await _gamepadService.GetManufacturersAsync();
            ViewBag.Feedbacks = await _gamepadService.GetFeedbacksAsync();
            ViewBag.ConnectionTypes = await _gamepadService.GetConnectionTypesAsync();
            ViewBag.CompatibleDevices = await _gamepadService.GetCompatibleDevicesAsync();
            var gamepads = await _gamepadService.GetPresentByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<GamepadViewModel>>(gamepads);
            await CheckIfInCartAsync(models);
            
            return View(models);
        }
        
        [HttpGet("gamepads/{id}")]
        public async Task<IActionResult> Gamepad(int id)
        {
            var gamepad = await _gamepadService.GetByIdAsync(id);
            if (gamepad == null)
                return NotFound();
            
            var model = _mapper.Map<GamepadViewModel>(gamepad);
            await CheckIfInCartAsync(model);
            
            return View(model);
        }
    }
}