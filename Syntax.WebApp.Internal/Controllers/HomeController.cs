using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
using System.Diagnostics;

namespace Syntax.WebApp.Internal.Controllers
{
    [AllowAnonymous]

    public class HomeController : BaseController
    {
        public HomeController(ILogger<HomeController> logger, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor)
        : base(baseLogger, httpContextAccessor)
        {
            _logger= logger;
            
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}