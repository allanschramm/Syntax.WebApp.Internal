using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
using System.Net.Http.Headers;

namespace Syntax.WebApp.Internal.Controllers
{
    public class AssetController : BaseController
    {
        HttpClient client;

        public AssetController(IHttpClientFactory factory, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor)
            : base(baseLogger, httpContextAccessor)
        {
            client = factory.CreateClient();

        }

        // GET: AssetController
        public async Task<ActionResult> Index()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/asset");
                if (response.IsSuccessStatusCode)
                {
                    
                    var listA = await response.Content.ReadAsAsync<Asset[]>();

                    return View(listA);
                }
                else
                {
                    throw new Exception("Ocorreu um erro na listagem!");
                }
            }
            catch (Exception ex)
            {
                return View("_Erro", ex);
            }
        }

        // GET: AssetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AssetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AssetController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/asset/{id}");
            if (response.IsSuccessStatusCode)
            {
                var assetClass = await response.Content.ReadAsAsync<Asset>();

                try
                {
                    var response1 = await client.GetAsync("api/assetclass/");
                    var listAC = await response1.Content.ReadAsAsync<AssetClass[]>();
                    ViewBag.AssetClass = listAC;

                }
                catch (Exception ex)
                {

                    throw ex;
                }
  
       
                return PartialView(assetClass);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro: ao localizar a Classe de Asset !";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AssetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AssetController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
