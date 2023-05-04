using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
using Syntax.WebApp.Internal.ViewModels;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Syntax.WebApp.Internal.Controllers
{
    public class AssetClassController : BaseController
    {
        HttpClient client;
        private readonly IWebHostEnvironment _env;


        public AssetClassController(IHttpClientFactory factory, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
            : base(baseLogger, httpContextAccessor)
        {
            client = factory.CreateClient();
            _env = env;
            

        }

        // GET: AssetClassController
        public async Task<ActionResult> Index()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://syntaxapi.azurewebsites.net/");
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
                return View("_Error", ex);
            }
        }


        // GET: AssetClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AssetClassController/Create
        public IActionResult Create()
        {
            var fileNames = Directory.GetFiles(Path.Combine(_env.WebRootPath, "Assets/AssetClass")).Select(Path.GetFileName);

            var fileList = fileNames.Select(fn => new FileViewModel
            {
                ImageUrl = Url.Content($"~/Assets/{fn}"),
                FileName = fn
            }).ToList();

            ViewBag.FileList = fileList;
            return PartialView();
        }

        // POST: AssetClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(AssetClass assetClass)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://syntaxapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));



            if (assetClass.IconFile != null && assetClass.IconFile.Length > 0)
            {
                var fileName = Path.GetFileName(assetClass.IconFile.FileName);
                var filePath = Path.Combine("wwwroot", "Assets/AssetClass", fileName);

                // Copia o arquivo para o diretório desejado
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await assetClass.IconFile.CopyToAsync(stream);
                }

                // Define o caminho relativo do arquivo para ser salvo na propriedade Icon
                assetClass.Icon = Path.Combine("Assets/AssetClass", fileName);
            }

            assetClass.IconFile = null;

            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(assetClass);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/assetclass", content);

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

        // GET: AssetClassController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://syntaxapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/assetclass/{id}");
            if (response.IsSuccessStatusCode)
            {
                var assetClass = await response.Content.ReadAsAsync<AssetClass>();

                var currentFileName = assetClass.Icon;

                var fileNames = Directory.GetFiles(Path.Combine(_env.WebRootPath, "Assets/AssetClass")).Select(Path.GetFileName);
                var fileList = fileNames.Select(fn => new FileViewModel
                {
                    ImageUrl = Url.Content($"~/Assets/AssetClass/{fn}"),
                    FileName = fn,
                    IsSelected = (fn == currentFileName)
                }).ToList();

                ViewBag.FileList = fileList;

                return PartialView(assetClass);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro: ao localizar a Classe de Asset !";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: AssetClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(AssetClass assetClass)
        {
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                client.BaseAddress = new Uri("https://syntaxapi.azurewebsites.net/");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var json = System.Text.Json.JsonSerializer.Serialize(assetClass);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PutAsync("api/assetclass", content);

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
        // GET: AssetClassController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://syntaxapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/assetclass/{id}");
            if (response.IsSuccessStatusCode)
            {
                var assetClass = await response.Content.ReadAsAsync<AssetClass>();

                // Chama a view de confirmação de deleção passando o objeto como parâmetro
                return PartialView(assetClass);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro ao localizar o User !";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: AssetClassController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("https://syntaxapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var response = await client.DeleteAsync($"api/assetclass/{id}");
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
