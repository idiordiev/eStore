using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eStore.ApplicationCore.FilterModels;
using eStore.ApplicationCore.Interfaces.Services;
using eStore.WebMVC.Models.Goods;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGoodsService _goodsService;
        private readonly IMapper _mapper;

        public GoodsController(IGoodsService goodsService, IMapper mapper)
        {
            _goodsService = goodsService;
            _mapper = mapper;
        }

        [HttpGet("keyboards")]
        public async Task<IActionResult> Keyboards([FromQuery] ICollection<int> manufacturerIds, 
            [FromQuery] ICollection<int> typeIds, 
            [FromQuery] ICollection<int> switchIds, 
            [FromQuery] ICollection<int> sizeIds)
        {
            var filter = new KeyboardFilterModel()
            {
                ManufacturerIds = manufacturerIds,
                KeyboardTypeIds = typeIds,
                SwitchIds = switchIds,
                KeyboardSizeIds = sizeIds
            };
            var keyboards = await _goodsService.GetKeyboardsByFilterAsync(filter);
            var models = _mapper.Map<IEnumerable<KeyboardViewModel>>(keyboards);
            return View(models);
        }
        
        [HttpGet("keyboards/{id}")]
        public async Task<IActionResult> Keyboard(int id)
        {
            var keyboard = await _goodsService.GetKeyboardByIdAsync(id);
            var model = _mapper.Map<KeyboardViewModel>(keyboard);
            return View(model);
        }

        [HttpGet("mouses")]
        public async Task<IActionResult> Mouses()
        {
            var mouses = await _goodsService.GetMousesAsync();
            var models = _mapper.Map<IEnumerable<MouseViewModel>>(mouses);
            return View(models);
        }
        
        [HttpGet("mouses/{id}")]
        public async Task<IActionResult> Mouse(int id)
        {
            var mouse = await _goodsService.GetMouseByIdAsync(id);
            var model = _mapper.Map<MouseViewModel>(mouse);
            return View(model);
        }
        
        [HttpGet("mousepads")]
        public async Task<IActionResult> Mousepads()
        {
            var mousepads = await _goodsService.GetMousepadsAsync();
            var models = _mapper.Map<IEnumerable<MousepadViewModel>>(mousepads);
            return View(models);
        }
        
        [HttpGet("mousepads/{id}")]
        public async Task<IActionResult> Mousepad(int id)
        {
            var mousepad = await _goodsService.GetMousepadByIdAsync(id);
            var model = _mapper.Map<MousepadViewModel>(mousepad);
            return View(model);
        }
        
        [HttpGet("gamepads")]
        public async Task<IActionResult> Gamepads()
        {
            var gamepads = await _goodsService.GetGamepadsAsync();
            var models = _mapper.Map<IEnumerable<GamepadViewModel>>(gamepads);
            return View(models);
        }
        
        [HttpGet("gamepads/{id}")]
        public async Task<IActionResult> Gamepad(int id)
        {
            var gamepad = await _goodsService.GetGamepadsAsync();
            var model = _mapper.Map<GamepadViewModel>(gamepad);
            return View(model);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}