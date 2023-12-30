using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Application.Filtering.Models;
using eStore.Application.Interfaces.Services;
using eStore.Infrastructure.Identity;
using eStore.WebMVC.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers;

public class KeyboardsController : Controller
{
    private readonly IGoodsService _goodsService;
    private readonly IKeyboardService _keyboardService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public KeyboardsController(
        IGoodsService goodsService,
        IKeyboardService keyboardService,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _goodsService = goodsService;
        _keyboardService = keyboardService;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index([FromQuery] KeyboardFilterModel filterModel)
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

    public async Task<IActionResult> Keyboard(int id)
    {
        var keyboard = await _keyboardService.GetByIdAsync(id);
        if (keyboard == null || keyboard.IsDeleted)
        {
            return NotFound();
        }

        var model = _mapper.Map<KeyboardViewModel>(keyboard);
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
            {
                model.IsAddedToCart = await _goodsService.CheckIfAddedToCartAsync(user.CustomerId, model.Id);
            }
        }
    }
}