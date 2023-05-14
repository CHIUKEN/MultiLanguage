# MutiLanguage Net Core

## Program.cs

### �]�w�귽�ɪ����|

```
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
```

### �]�w�䴩���y�t

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

�b *RequestLocalizationOptions* ���ѼƳ]�w���A���@�� `RequestCultureProviders` ���Ѽƫ����O���ѻy�t�ӷ����覡�w�]��3�ءC

- QueryStringRequestCultureProvider
- CookieRequestCultureProvider
- AcceptLanguageHeaderRequestCultureProvider

�w�]�����涶�ǡGQueryString => Cookie => Header

#### QueryStringRequestCultureProvider

�q QueryString �����]�w�y�t�C

```
https://yourdomain.com?culture=en-us
```

#### CookieRequestCultureProvider
Cookie ���y�t�P�_�� Cookie �W�٬O **.AspNetCore.Culture** �A�Ȫ��W�h�O **c=<lang>|uic=<lang>** �A �ҡG c=zh-tw|uic=zh-tw �C

�Y�n��� cookie �A�N culture �אּ�䥦�y�t
```
    Response.Cookies.Append(  
        CookieRequestCultureProvider.DefaultCookieName,  
        CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),  
        new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) } 
    );
```

#### AcceptLanguageHeaderRequestCultureProvider

�O�� Header ���� **Accept-Languge** ���Ȩӽվ�y�t�C


## �귽��

�s�W *Resourcs* ��Ƨ��A�w�] �� Views ������ƶi��y�t�]�w�A�Ш̾� Views �������c�i���Ƨ��s�W�C�ҡGViews/Home/Index.cshtml �N�n�bResources�s�W *Views/Home/Index.<lang>.en-us.resx*

### Controller ����
DI *IStringLocalizer<Controller Name>* �b Controller ���غc�l���A�A�ھ� Resource ���w�q�� Key �ȨӨ����

```
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

### Views ���� 
Inject `IViewLocalizer` �A�� Key �Ȩ����
```
@inject IViewLocalizer _localizer

<h1>@_localizer["Welcome"]</h1>
```

### ShareResource

�@�θ�Ƥ��ھ�Controlle �άO Views �ӦU�۷s�W�C���s�W `SharedResources.cs` ���Ū� cs �ɡA���M�׬O�s�W�b `MultiLanguageNetCore.Resources` �o�� namespace ���A�ҥH�������y�t�ɤ]�n�b������namespace���A�ҥH�s�W�b `Resources/Resources/SharedResources.<lang>.resx` �o�ӵ��c�U�C

## �ѦҸ��

- [ASP.NET Core localization and translation with examples](https://lokalise.com/blog/asp-net-core-localization/)
- [Globalization and Localization in ASP.NET Core �V Detailed](https://codewithmukesh.com/blog/globalization-and-localization-in-aspnet-core/)
- [ASP.NET Core �����Υ[�W�h��y�t�Υ��a��](https://www.dotblogs.com.tw/Null/2020/05/05/155552)
- [Building Multilingual Applications in ASP.NET Core](https://www.ezzylearning.net/tutorial/building-multilingual-applications-in-asp-net-core)