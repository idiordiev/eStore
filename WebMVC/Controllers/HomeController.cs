using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace eStore.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return await Task.Run(View);
        }

        public async Task<IActionResult> Services()
        {
            return await Task.Run(View);
        }

        public async Task<IActionResult> Faq()
        {
            return await Task.Run(View);
        }
        
        public async Task<IActionResult> Error()
        {
            return await Task.Run(View);
        }
    }
}