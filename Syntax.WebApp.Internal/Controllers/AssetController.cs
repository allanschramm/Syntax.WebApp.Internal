using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

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
            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
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
                return View("_Error", ex);
            }
        }

        // GET: AssetController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetController/Create
        public async Task<ActionResult> Create()
        {
            var assetClassList = await GetAssetClassList();
            ViewBag.AssetClass = assetClassList;
            return PartialView();
        }

        // POST: AssetController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Asset asset)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(asset);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/asset", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessages"] = new[] { "Sucesso: Criado com Sucesso." };

                    // Sucesso
                    return RedirectToAction(nameof(Index));
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var jsonErr = await response.Content.ReadAsStringAsync();
                    var apiError = JsonSerializer.Deserialize<ApiError>(jsonErr, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;

                    var errorMessages = apiError.ErrorMessages;

                    TempData["ErrorMessages"] = errorMessages.ToArray();
                }
                else
                {
                    // Falha
                    throw new Exception("Ocorreu um erro na listagem!");
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: AssetController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var assetClassList = await GetAssetClassList();
            ViewBag.AssetClass = assetClassList;
            var response = await client.GetAsync($"api/asset/{id}");
            if (response.IsSuccessStatusCode)
            {
                var assetClass = await response.Content.ReadAsAsync<Asset>();

                return PartialView(assetClass);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro: ao localizar a Classe de Asset !";
                return RedirectToAction(nameof(Index));
            }
        }
        private async Task<List<AssetClass>> GetAssetClassList()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync("api/assetclass/");
            var listAC = await response.Content.ReadAsAsync<List<AssetClass>>();

            return listAC;
        }

        // POST: AssetController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Asset asset)
        {
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(asset);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("api/asset", content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Sucesso
                        TempData["SuccessMessages"] = new[] { "Sucesso: Dados atualizados com sucesso." };

                        return RedirectToAction(nameof(Index));
                    }
                    if (response.StatusCode == HttpStatusCode.BadRequest)
                    {
                        var jsonErr = await response.Content.ReadAsStringAsync();
                        var apiError = System.Text.Json.JsonSerializer.Deserialize<ApiError>(jsonErr, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                        var errorMessages = apiError!.ErrorMessages;


                        TempData["ErrorMessages"] = errorMessages.ToArray();
                    }
                    else
                    {
                        // Falha
                        throw new Exception("Ocorreu um erro na listagem!");
                    }
                }
                catch
                {
                    return RedirectToAction(nameof(Index));
                }

                return RedirectToAction(nameof(Index));
            }
        }

        // GET: AssetController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/asset/{id}");
            if (response.IsSuccessStatusCode)
            {
                var assetClass = await response.Content.ReadAsAsync<Asset>();

                // Chama a view de confirmação de deleção passando o objeto como parâmetro
                return PartialView(assetClass);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro ao localizar o User !";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AssetController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var response = await client.DeleteAsync($"api/asset/{id}");
                if (response.IsSuccessStatusCode)
                {
                    // Sucesso
                    TempData["SuccessMessages"] = new[] { "Sucesso: Dados atualizados com sucesso." };

                    return RedirectToAction(nameof(Index));
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var jsonErr = await response.Content.ReadAsStringAsync();
                    var apiError = JsonSerializer.Deserialize<ApiError>(jsonErr, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    var errorMessages = apiError!.ErrorMessages!;

                    TempData["ErrorMessages"] = errorMessages.ToArray();
                }
                else
                {
                    // Falha
                    throw new Exception("Ocorreu um erro na listagem!");
                }
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
