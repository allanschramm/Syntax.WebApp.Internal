﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Syntax.WebApp.Internal.Models;
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
            return PartialView();
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
                    var apiError = JsonSerializer.Deserialize<ApiError>(jsonErr, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

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
        public ActionResult Edit(int id)
        {
            return PartialView();
        }

        // POST: AssetClassController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(AssetClass assetClass)
        {
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
                client.BaseAddress = new Uri("http://localhost:5069");
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
        // GET: AssetClassController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            client.BaseAddress = new Uri("http://localhost:5069");
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
            client.BaseAddress = new Uri("http://localhost:5069");
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
