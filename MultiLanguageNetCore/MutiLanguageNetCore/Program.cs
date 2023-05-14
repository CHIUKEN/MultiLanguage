using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});

builder.Services.AddControllersWithViews()
    .AddViewLocalization();

var supportedCultures = new List<CultureInfo>()
{
	new CultureInfo("zh-tw"),
	new CultureInfo("en-us"),
};

builder.Services.Configure<RequestLocalizationOptions>(options => {
	options.DefaultRequestCulture = new RequestCulture("zh-tw");
    //options.RequestCultureProviders
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{culture=zh-tw}/{controller=Home}/{action=Index}/{id?}", constraints: new { culture = "zh-tw|en-us" });

app.Run();
