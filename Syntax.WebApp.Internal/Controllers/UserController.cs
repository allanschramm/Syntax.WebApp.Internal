using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syntax.WebApp.Internal.Models;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Syntax.WebApp.Internal.Controllers
{
    public class UserController : BaseController
    {
        HttpClient client;

        public UserController(IHttpClientFactory factory, ILogger<BaseController> baseLogger, IHttpContextAccessor httpContextAccessor)
            : base(baseLogger, httpContextAccessor)
        {
            client = factory.CreateClient();

        }

        // GET: UserController
        public async Task<ActionResult> Index()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                HttpResponseMessage response = client.GetAsync("api/user").Result;
                if (response.IsSuccessStatusCode)
                {
                    var listUser = await response.Content.ReadAsAsync<User[]>();
                    return View(listUser.ToList());
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

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Create
        public ActionResult CreateAsync()
        {
            return PartialView();
        }

        // POST: UserController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(User user)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                
                var json = System.Text.Json.JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("api/user/register", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessages"] = new[] { "Sucesso: Criado com Sucesso." };

                    // Sucesso
                    return RedirectToAction(nameof(Index));
                }
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var jsonErr = await response.Content.ReadAsStringAsync();
                    var apiError = System.Text.Json.JsonSerializer.Deserialize<ApiError>(jsonErr, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

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


        // GET: UserController/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<User>();

                return PartialView(user);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro: ao localizar o User !";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(User user)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var json = System.Text.Json.JsonSerializer.Serialize(user);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PutAsync("api/user", content);

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

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.GetAsync($"api/User/{id}");
            if (response.IsSuccessStatusCode)
            {
                var user = await response.Content.ReadAsAsync<User>();

                // Chama a view de confirmação de deleção passando o objeto como parâmetro
                return PartialView(user);
            }
            else
            {
                TempData["ErrorMessages"] = "Erro ao localizar o User !";
                return RedirectToAction(nameof(Index));
            }

        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            try
            {
                var response = await client.DeleteAsync($"api/user/{id}");
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

    }

}
