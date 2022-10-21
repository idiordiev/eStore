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
        public GoodsController()
        {
        }

        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(View());
        }
    }
}