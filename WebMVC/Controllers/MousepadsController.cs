using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Services;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class MousepadsController : Controller
    {
        private readonly IMousepadService _mousepadService;
        private readonly IGoodsService _goodsService;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public MousepadsController(IMousepadService mousepadService,
            IMapper mapper, 
            UserManager<ApplicationUser> userManager, 
            IGoodsService goodsService)
        {
            _mousepadService = mousepadService;
            _mapper = mapper;
            _userManager = userManager;
            _goodsService = goodsService;
        }

        public async Task<IActionResult> Index([FromQuery] MousepadFilterModel filterModel)
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
        
        public async Task<IActionResult> Mousepad(int id)
        {
            var mousepad = await _mousepadService.GetByIdAsync(id);
            if (mousepad == null || mousepad.IsDeleted) 
                return NotFound();

            var model = _mapper.Map<MousepadViewModel>(mousepad);
            await CheckIfInCartAsync(model);

            return View(model);
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
    }
}