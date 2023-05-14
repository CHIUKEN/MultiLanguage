# MutiLanguage Net Core

## Program.cs

### 設定資源檔的路徑

```cs
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
```

### 設定支援的語系

```cs
var supportedCultures = new List<CultureInfo>()
{
	new CultureInfo("zh-tw"),
	new CultureInfo("en-us"),
};

builder.Services.Configure<RequestLocalizationOptions>(options => {
	options.DefaultRequestCulture = new RequestCulture("zh-tw");
	options.SupportedCultures = supportedCultures;
	options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

app.UseRequestLocalization(app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);
```

### RequestLocalizationOptions

在 *RequestLocalizationOptions* 的參數設定中，有一個 `RequestCultureProviders` 的參數指的是提供語系來源的方式預設有3種。

- QueryStringRequestCultureProvider
- CookieRequestCultureProvider
- AcceptLanguageHeaderRequestCultureProvider

預設的執行順序：QueryString => Cookie => Header

#### QueryStringRequestCultureProvider

從 QueryString 直接設定語系。

```
https://yourdomain.com?culture=en-us
```

#### CookieRequestCultureProvider
Cookie 的語系判斷的 Cookie 名稱是 **.AspNetCore.Culture** ，值的規則是 **c=&lt;lang&gt;|uic=&lt;lang&gt;** ， 例： c=zh-tw|uic=zh-tw 。

若要更改 cookie ，將 culture 改為其它語系
```cs
    Response.Cookies.Append(  
        CookieRequestCultureProvider.DefaultCookieName,  
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),  
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) } 
    );
```

#### AcceptLanguageHeaderRequestCultureProvider

是由 Header 中的 **Accept-Languge** 的值來調整語系。


## 資源檔

新增 *Resourcs* 資料夾，預設 對 Views 中的資料進行語系設定，請依據 Views 中的結構進行資料夾新增。例：Views/Home/Index.cshtml 就要在Resources新增 *Views/Home/Index.<lang>.en-us.resx*

### Controller 取值
DI *IStringLocalizer<Controller Name>* 在 Controller 的建構子中，再根據 Resource 中定義的 Key 值來取資料

```cs
private readonly IStringLocalizer<HomeController> _stringLocalizer;

public HomeController(ILogger<HomeController> logger, IStringLocalizer<HomeController> stringLocalizer)
{
	_logger = logger;
	_stringLocalizer = stringLocalizer;
}

public IActionResult Index()
{
    var title = _stringLocalizer["Title"];
    return View();
}
```

### Views 取值 
Inject `IViewLocalizer` 再用 Key 值取資料
```cs
@inject IViewLocalizer _localizer

<h1>@_localizer["Welcome"]</h1>
```

### ShareResource

共用資料不根據Controlle 或是 Views 來各自新增。先新增 `SharedResources.cs` 為空的 cs 檔，此專案是新增在 `MultiLanguageNetCore.Resources` 這個 namespace 中，所以對應的語系檔也要在對應的namespace中，所以新增在 `Resources/Resources/SharedResources.<lang>.resx` 這個結構下。

## 參考資料

- [ASP.NET Core localization and translation with examples](https://lokalise.com/blog/asp-net-core-localization/)
- [Globalization and Localization in ASP.NET Core – Detailed](https://codewithmukesh.com/blog/globalization-and-localization-in-aspnet-core/)
- [ASP.NET Core 為應用加上多國語系及本地化](https://www.dotblogs.com.tw/Null/2020/05/05/155552)
- [Building Multilingual Applications in ASP.NET Core](https://www.ezzylearning.net/tutorial/building-multilingual-applications-in-asp-net-core)