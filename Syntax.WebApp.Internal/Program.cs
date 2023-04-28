using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Session;
using Syntax.WebApp.Internal.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(option => { option.ResourcesPath = "Resource"; });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();
builder.Services.AddHttpContextAccessor();


builder.Services.Configure<RequestLocalizationOptions>(option =>
{
    var vSupportCultures = new List<CultureInfo>
                    {
                        new CultureInfo("en"),
                        new CultureInfo("pt-BR"),
                        new CultureInfo("es")
                    };
    option.DefaultRequestCulture = new RequestCulture("en");
    option.SupportedCultures = vSupportCultures;
    option.SupportedUICultures = vSupportCultures;
});

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

// Service for HttpClient
builder.Services.AddHttpClient();

builder.Services.AddDistributedMemoryCache(); // adiciona o servi�o de cache distribu�do em mem�ria
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // define o tempo de vida da sess�o
    options.Cookie.HttpOnly = true; // define se o cookie da sess�o s� pode ser acessado via HTTP
    options.Cookie.IsEssential = true; // define se o cookie da sess�o � essencial para a aplica��o
});


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession();


app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseMiddleware<TokenRequest>();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
