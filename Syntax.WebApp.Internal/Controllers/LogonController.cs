using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Syntax.WebApp.Internal.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Syntax.WebApp.Internal.Controllers
{

    public class LogonController : Controller
    {
        HttpClient client;

        public LogonController(IHttpClientFactory factory)
        {
            client = factory.CreateClient();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logon(Logon logon)
        {

            client.BaseAddress = new Uri("http://syntaxinternal.azurewebsites.net:5069");
            client.DefaultRequestHeaders.Accept.Add(new
                MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                var json = JsonConvert.SerializeObject(logon);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync("api/user/login", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    
                    var tokenString = await response.Content.ReadAsStringAsync();
                    TokenClass token = JsonConvert.DeserializeObject<TokenClass>(tokenString)!;
                    DateTime? expirationDate = token.ExpirationDate;



                    HttpContext.Session.SetString("SyntaxToken", token.Token);
                    // Set the authentication header. 
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    string erro = $"{response.StatusCode} - {response.ReasonPhrase}";
                    return View("_Error", erro);
                }
            }
            catch (Exception ex)
            {
                return View("_Error", ex.Message);
            }
        }

        // GET: LogonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LogonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LogonController/Create
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

        // GET: LogonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LogonController/Edit/5
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

        // GET: LogonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LogonController/Delete/5
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
