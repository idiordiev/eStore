using System.Threading.Tasks;
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