using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syntax.WebApp.Internal.Models;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Headers;

namespace Syntax.WebApp.Internal.Controllers
{
    [AllowAnonymous]

    public class HomeController : BaseController
    {
        HttpClient client;

        public HomeController(ILogger<HomeController> logger, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor, IHttpClientFactory factory)
        : base(baseLogger, httpContextAccessor)
        {
            _logger = logger;
            client = factory.CreateClient();

        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<JsonResult> GetDashboard()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/dashboard/TCbyMonth");
                if (response.IsSuccessStatusCode)
                {
                    var usuarios = await response.Content.ReadAsAsync<IEnumerable<dynamic>>();
                    return Json(usuarios);
                }
                else
                {
                    throw new Exception("Ocorreu um erro na listagem!");
                }
            }
            catch (Exception ex)
            {
                return Json(null);
            }
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}