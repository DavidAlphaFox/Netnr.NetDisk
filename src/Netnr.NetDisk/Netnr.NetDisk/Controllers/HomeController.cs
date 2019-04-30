using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Netnr.NetDisk.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect(GlobalTo.GetValue("path:index"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
