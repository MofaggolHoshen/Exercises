## Localization using Resource file
Be carefull about naming convention and resources folder hierarchy. For modetails see Microsoft documention.
### Startup.cs
````C#
public void ConfigureServices(IServiceCollection services)
        {
            // Add the localization services to the services container
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddMvc()
                // Add support for finding localized views, based on file name suffix, e.g. Index.fr.cshtml
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                // Add support for localizing strings in data annotations (e.g. validation messages) via the
                // IStringLocalizer abstractions.
                .AddDataAnnotationsLocalization();

            // Configure supported cultures and localization options
            services.Configure<RequestLocalizationOptions>(options =>
            {
                var supportedCultures = new List<CultureInfo>()
                {
                    new CultureInfo("en-US"),
                    new CultureInfo("fr"),
                    new CultureInfo("ar-YE"),
                    new CultureInfo("de-DE")
                };

                // State what the default culture for your application is. This will be used if no specific culture
                // can be determined for a given request.
                options.DefaultRequestCulture = new RequestCulture(culture: "en-US", uiCulture: "en-US");

                // You must explicitly state which cultures your application supports.
                // These are the cultures the app supports for formatting numbers, dates, etc.
                options.SupportedCultures = supportedCultures;

                // These are the cultures the app supports for UI strings, i.e. we have localized resources for.
                options.SupportedUICultures = supportedCultures;

                // You can change which providers are configured to determine the culture for requests, or even add a custom
                // provider with your own logic. The providers will be asked in order to provide a culture for each request,
                // and the first to provide a non-null result that is in the configured supported cultures list will be used.
                // By default, the following built-in providers are configured:
                // - QueryStringRequestCultureProvider, sets culture via "culture" and "ui-culture" query string values, useful for testing
                // - CookieRequestCultureProvider, sets culture via "ASPNET_CULTURE" cookie
                // - AcceptLanguageHeaderRequestCultureProvider, sets culture via the "Accept-Language" request header
                //options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(async context =>
                //{ 
                //  // My custom request culture logic
                //  return new ProviderCultureResult("en");
                //}));
            });
        }
````
````C#
public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // Middleware for Localization with options
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
        }
````

### Controller 

````C#
public class HomeController : Controller
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public HomeController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            var vi = _localizer["Your application description page."];

            ViewData["Message"] = vi;

            return View();
        }
````

### View
````C#
@using Microsoft.AspNetCore.Mvc.Localization

@* The IViewLocalizer can be injected into the view. It does two things of interest:
    1. It can HTML encode *parameters* passed to resource strings (not the resource strings themselves, as they may contain
       HTML) automatically, and return the result as an IHtmlContent so Razor won't HTML encode it again;
    2. It looks for localization resources for this view based on the view path, e.g. if the view's path is
       "MyApplication/Views/Home/Index.cshtml", then the corresponding resource base path will be "MyApplication.Views.Home.Index" *@

@inject IViewLocalizer Localizer

<p>
 @Localizer["Learn More"]
</p>
````