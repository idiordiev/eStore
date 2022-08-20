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
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice)
        {
            var filter = new KeyboardFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                KeyboardTypeIds = typeIds,
                SwitchIds = switchIds,
                KeyboardSizeIds = sizeIds,
                MinPrice = minPrice,
                MaxPrice = maxPrice
            };
            var keyboards = await _keyboardService.GetAllByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<KeyboardViewModel>>(keyboards);
            ViewBag.Manufacturers = await _keyboardService.GetManufacturersAsync();
            ViewBag.Sizes = await _keyboardService.GetSizesAsync();
            ViewBag.Types = await _keyboardService.GetTypesAsync();
            ViewBag.Switches = await _keyboardService.GetSwitchesAsync();
            return View(models);
        }
        
        [HttpGet("keyboards/{id}")]
        public async Task<IActionResult> Keyboard(int id)
        {
            var keyboard = await _keyboardService.GetByIdAsync(id);
            var model = _mapper.Map<KeyboardViewModel>(keyboard);
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, id);
            }
            
            return View(model);
        }

        [HttpGet("mouses")]
        public async Task<IActionResult> Mouses([FromQuery] ICollection<int> manufacturerIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] int? minWeight,
            [FromQuery] int? maxWeight)
        {
            var filter = new MouseFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                MaxWeight = maxWeight,
                MinWeight = minWeight
            };
            ViewBag.Manufacturers = await _mouseService.GetManufacturersAsync();
            var mouses = await _mouseService.GetAllByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<MouseViewModel>>(mouses);
            return View(models);
        }
        
        [HttpGet("mouses/{id}")]
        public async Task<IActionResult> Mouse(int id)
        {
            var mouse = await _mouseService.GetByIdAsync(id);
            var model = _mapper.Map<MouseViewModel>(mouse);
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, id);
            }
            return View(model);
        }
        
        [HttpGet("mousepads")]
        public async Task<IActionResult> Mousepads([FromQuery] ICollection<int> manufacturerIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] ICollection<int> topMaterialIds,
            [FromQuery] ICollection<int> bottomMaterialIds,
            [FromQuery] ICollection<bool> isStitchedValues)
        {
            var filter = new MousepadFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                TopMaterialIds = topMaterialIds,
                BottomMaterialIds = bottomMaterialIds,
                IsStitchedValues = isStitchedValues
            };
            ViewBag.Manufacturers = await _mousepadService.GetManufacturersAsync();
            ViewBag.TopMaterials = await _mousepadService.GetTopMaterialsAsync();
            ViewBag.BottomMaterials = await _mousepadService.GetBottomMaterialsAsync();
            var mousepads = await _mousepadService.GetAllByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<MousepadViewModel>>(mousepads);
            return View(models);
        }
        
        [HttpGet("mousepads/{id}")]
        public async Task<IActionResult> Mousepad(int id)
        {
            var mousepad = await _mousepadService.GetByIdAsync(id);
            var model = _mapper.Map<MousepadViewModel>(mousepad);
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, id);
            }
            return View(model);
        }
        
        [HttpGet("gamepads")]
        public async Task<IActionResult> Gamepads([FromQuery] ICollection<int> manufacturerIds,
            [FromQuery] decimal? minPrice,
            [FromQuery] decimal? maxPrice,
            [FromQuery] ICollection<int> connectionTypeIds,
            [FromQuery] ICollection<int> feedbackIds)
        {
            var filter = new GamepadFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                MaxPrice = maxPrice,
                MinPrice = minPrice,
                ConnectionTypeIds = connectionTypeIds,
                FeedbackIds = feedbackIds
            };
            ViewBag.Manufacturers = await _gamepadService.GetManufacturersAsync();
            ViewBag.Feedbacks = await _gamepadService.GetFeedbacksAsync();
            ViewBag.ConnectionTypes = await _gamepadService.GetConnectionTypesAsync();
            var gamepads = await _gamepadService.GetAllByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<GamepadViewModel>>(gamepads);
            return View(models);
        }
        
        [HttpGet("gamepads/{id}")]
        public async Task<IActionResult> Gamepad(int id)
        {
            var gamepad = await _gamepadService.GetByIdAsync(id);
            var model = _mapper.Map<GamepadViewModel>(gamepad);
            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, id);
            }
            return View(model);
        }
    }
}