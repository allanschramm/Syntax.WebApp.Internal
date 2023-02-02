using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(option => { option.ResourcesPath = "Resource"; });
builder.Services.AddMvc().AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix).AddDataAnnotationsLocalization();

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

// Service for HttpClient
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
