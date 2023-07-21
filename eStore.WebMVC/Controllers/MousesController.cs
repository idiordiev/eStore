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

namespace eStore.WebMVC.Controllers;

public class MousesController : Controller
{
    private readonly IGoodsService _goodsService;
    private readonly IMouseService _mouseService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public MousesController(IGoodsService goodsService,
        IMouseService mouseService,
        IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _goodsService = goodsService;
        _mouseService = mouseService;
        _mapper = mapper;
        _userManager = userManager;
    }

    public async Task<IActionResult> Index([FromQuery] MouseFilterModel filterModel)
    {
        ViewBag.Manufacturers = await _mouseService.GetManufacturersAsync();
        ViewBag.Backlights = await _mouseService.GetBacklightsAsync();
        ViewBag.ConnectionTypes = await _mouseService.GetConnectionTypesAsync();
        var mouses = await _mouseService.GetPresentByFilterAsync(filterModel);
        var models = _mapper.Map<IEnumerable<MouseViewModel>>(mouses);
        await CheckIfInCartAsync(models);

        return View(models);
    }

    public async Task<IActionResult> Mouse(int id)
    {
        Mouse mouse = await _mouseService.GetByIdAsync(id);
        if (mouse == null || mouse.IsDeleted)
        {
            return NotFound();
        }

        var model = _mapper.Map<MouseViewModel>(mouse);
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