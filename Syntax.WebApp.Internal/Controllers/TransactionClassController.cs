using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Web;


namespace Syntax.WebApp.Internal.Controllers
{
    public class TransactionClassController : BaseController
    {
        HttpClient client;
        private readonly IWebHostEnvironment _env;

        public TransactionClassController(IHttpClientFactory factory, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment env)
            : base(baseLogger, httpContextAccessor)
        {
            client = factory.CreateClient();
            _env = env;
        }

        // GET: TransactionClassController
        public async Task<ActionResult> Index()
        {
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync("api/transactionclass").Result;
                if (response.IsSuccessStatusCode)
                {
                    var listTC = await response.Content.ReadAsAsync<TransactionClass[]>();
                    return View(listTC.ToList());
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

        // GET: TransactionClassController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TransactionClassController/Create
        public ActionResult Create()
        {
            var fileNames = Directory.GetFiles(_env.WebRootPath, "Assets/*.*").Select(Path.GetFileName);
            var fileList = fileNames.Select(fn => new
            {
                ImageUrl = Url.Content($"~/Assets/{fn}"),
                FileName = fn
            });

            ViewBag.FileList = fileList;


            return PartialView();
        }

        // POST: TransactionClassController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(TransactionClass transactionClass)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


            if (transactionClass.IconFile != null && transactionClass.IconFile.Length > 0)
            {
                var fileName = Path.GetFileName(transactionClass.IconFile.FileName);
                var filePath = Path.Combine("wwwroot", "Assets", fileName);

                // Copia o arquivo para o diretório desejado
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await transactionClass.IconFile.CopyToAsync(stream);
                }

                // Define o caminho relativo do arquivo para ser salvo na propriedade Icon
                transactionClass.Icon = Path.Combine("Assets", fileName);
            }

            transactionClass.IconFile = null;


            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(transactionClass);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/transactionclass", content);
           

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
        // GET: TransactionClassController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TransactionClassController/Edit/5
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

        // GET: TransactionClassController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TransactionClassController/Delete/5
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
