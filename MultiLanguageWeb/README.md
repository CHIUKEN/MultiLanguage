# .Net Mvc MultiLanguge

## Route

更新 RouteConfig.cs，新增 `culture` 在url參數中。*constraints* 是來約束 culture 參數，若不是在 contrains 中則會回傳 404。
```cs
routes.MapRoute(
    name: "Default",
    url: "{culture}/{controller}/{action}/{id}",
    defaults: new
    {
        culture = "zh-tw",
        controller = "Home",
        action = "Index",
        id = UrlParameter.Optional
    },
    constraints: new { culture = "zh-tw|en-us" }
);
```

## BaseController

目的是覆寫 `OnActionExecuting` 這個 function，覆寫這個function 來處理 Cookie 及 Url 中的參數。之後每一個 Controller 再繼承 BaseController 。

## 資料檔

新增 Resources 資料夾(也不一定必須為這個名稱，這個只是個 namespace )，這個資料夾中放的是 `.resx` 的副檔名，檔案名稱規則 *Resources.{culture-code}.resx*  ，存取修飾詞(Access Modify) 須調整為 **public**

### 更改 Views/Web.config

新增 Resources 的命名空間在 Web.config 中，為的是在拿 Resources 中的變數，可以少寫 namespace

```xml
  <system.web.webPages.razor>
    <host factoryType="System.Web.Mvc.MvcWebRazorHostFactory, System.Web.Mvc, Version=5.2.9.0, Culture=neutral, PublicKeyToken=31BF3856AD364E35" />
    <pages pageBaseType="System.Web.Mvc.WebViewPage">
      <namespaces>
        <add namespace="System.Web.Mvc" />
        <add namespace="System.Web.Mvc.Ajax" />
        <add namespace="System.Web.Mvc.Html" />
        <add namespace="System.Web.Optimization"/>
        <add namespace="System.Web.Routing" />
        <add namespace="MultiLanguageWeb" />
		<add namespace="MultiLanguageWeb.Resources"/>
      </namespaces>
    </pages>
  </system.web.webPages.razor>
```

## Views
在需跟相對應的語系進行調整的地方，使用 *Resources.Your Setting String* 來使用。

目前這個專案中的Resource有設定 `AppName`，所以在 *_Layout.cshtml* 就可以用 `Resources.AppName` 來拿相對應的值。

```cs
@Html.ActionLink(Resources.AppName, "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
```

## Global.asax

在 Global.asax 中 `Application_BeginRequest` 是應用程式的第一個進入點，也可以從這裡取得 `culture` Cookie 的值。

# 參考資料
- [Get insight to build your first Multi-Language ASP.NET MVC 5 Web Application](https://www.codeproject.com/Articles/1160340/Get-insight-to-build-your-first-Multi-Language-ASP)
- [Getting Started With ASP.NET MVC i18n](https://phrase.com/blog/posts/getting-started-with-asp-net-mvc-i18n/)
- [ASP.NET MVC - 使用 MVC 實作多國語系](https://dotblogs.com.tw/dc690216/2009/11/04/11401)