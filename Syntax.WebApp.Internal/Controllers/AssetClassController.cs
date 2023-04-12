using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Syntax.WebApp.Internal.Controllers
{
    public class AssetClassController : BaseController
    {
        HttpClient client;
        

        public AssetClassController(IHttpClientFactory factory, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor)
            : base(baseLogger, httpContextAccessor)
        {
            client = factory.CreateClient();

        }

        // GET: AssetClassController
        public async Task<ActionResult> Index()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = await client.GetAsync("api/assetclass");
                if (response.IsSuccessStatusCode)
                {
                    var listAC = await response.Content.ReadAsAsync<AssetClass[]>();

                    return View(listAC);
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


        // GET: AssetClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetClassController/Create
        public async Task<ActionResult> CreateAsync()
        {
            return View();
        }

        // POST: AssetClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AssetClass assetClass)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var json = JsonSerializer.Serialize(assetClass);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync("api/assetclass", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
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

        // GET: AssetClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AssetClassController/Edit/5
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

        // GET: AssetClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AssetClassController/Delete/5
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
