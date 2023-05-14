# .Net Mvc MultiLanguge

## Route

��s RouteConfig.cs�A�s�W `culture` �burl�ѼƤ��C*constraints* �O�Ӭ��� culture �ѼơA�Y���O�b contrains ���h�|�^�� 404�C
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

�ت��O�мg `OnActionExecuting` �o�� function�A�мg�o��function �ӳB�z Cookie �� Url �����ѼơC����C�@�� Controller �A�~�� BaseController �C

## �����

�s�W Resources ��Ƨ�(�]���@�w�������o�ӦW�١A�o�ӥu�O�� namespace )�A�o�Ӹ�Ƨ����񪺬O `.resx` �����ɦW�A�ɮצW�ٳW�h *Resources.{culture-code}.resx*  �A�s���׹���(Access Modify) ���վ㬰 **public**

### ��� Views/Web.config

�s�W Resources ���R�W�Ŷ��b Web.config ���A�����O�b�� Resources �����ܼơA�i�H�ּg namespace

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
�b�ݸ�۹������y�t�i��վ㪺�a��A�ϥ� *Resources.Your Setting String* �ӨϥΡC

�ثe�o�ӱM�פ���Resource���]�w `AppName`�A�ҥH�b *_Layout.cshtml* �N�i�H�� `Resources.AppName` �Ӯ��۹������ȡC

```
@Html.ActionLink(Resources.AppName, "Index", "Home", new { area = "" }, new { @class = "navbar-brand" })
```

## Global.asax

�b Global.asax �� `Application_BeginRequest` �O���ε{�����Ĥ@�Ӷi�J�I�A�]�i�H�q�o�̨��o `culture` Cookie ���ȡC

# �ѦҸ��
- [Get insight to build your first Multi-Language ASP.NET MVC 5 Web Application](https://www.codeproject.com/Articles/1160340/Get-insight-to-build-your-first-Multi-Language-ASP)
- [Getting Started With ASP.NET MVC i18n](https://phrase.com/blog/posts/getting-started-with-asp-net-mvc-i18n/)
- [ASP.NET MVC - �ϥ� MVC ��@�h��y�t](https://dotblogs.com.tw/dc690216/2009/11/04/11401)