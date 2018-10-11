using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApplication5.Models;
using Microsoft.AspNetCore.Http.Extensions;
using WebApplication5.Helper;
using Microsoft.EntityFrameworkCore;

namespace WebApplication5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddSingleton<IBaseUrlContext,BaseUrlContext>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});

            services.AddMultitenancy<AppTenant, CachingAppTenantResolver>();
            
            services.AddDbContext<DataContext>();

            services.AddAuthentication(AzureADDefaults.AuthenticationScheme)
                .AddCookie()
                .AddAzureAD(options => Configuration.Bind("AzureAd", options));

            services.Configure<OpenIdConnectOptions>(AzureADDefaults.OpenIdScheme, options =>
            {
                //options.Prompt = "admin_consent";

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    

                    IssuerValidator = (issuer, securityToken, tokenValidationParameter) =>
                    {
                        try
                        {
                            var multitenancyOptions = new MultitenancyOptions();

                            
                            Configuration.Bind("Multitenancy", multitenancyOptions);

                            //User tenant information
                            var tenant = multitenancyOptions.Tenants.Single(t => issuer.Contains(t.TenantId));

                            // User claims
                            var token = (securityToken as JwtSecurityToken).Claims.ToList();

                            //User domain name from his unique name
                            var userDomain = token.Single(i => i.Type == "unique_name").Value.Split('@')[1];
                            
                            //Url from where request is coming 
                            var url = services.BuildServiceProvider().GetService<IHttpContextAccessor>().HttpContext.Request.GetDisplayUrl().Split('/')[2];

                            //TODO: Need to check requested sub domain is equal to user email domain
                            if(!tenant.Hostnames.Any(i=> i == url))
                                throw new SecurityTokenInvalidIssuerException($"Issuer {issuer} not allowed.");

                            return issuer;
                        }
                        catch (Exception ex)
                        {
                            throw new SecurityTokenInvalidIssuerException($"Issuer {issuer} not allowed.");
                        }
                    }

                };

                options.Events = new OpenIdConnectEvents
                {
                    OnTicketReceived = context =>
                    {
                        // If your authentication logic is based on users then add your logic here
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.HandleResponse();

                        context.Response.Redirect("/Home/AuthError?message=" + context.Exception.Message);

                        //context.Response.Redirect("/AzureAD/Account/SignOut");

                        return Task.CompletedTask;
                    }
                };
            });
            
            services.AddMvc(
                options =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //    .RequireAuthenticatedUser()
                //    .Build();
                //options.Filters.Add(new AuthorizeFilter(policy));

                options.Filters.Add(typeof(AnalyticsAuthorizeAttribute));
            }
            )
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.Configure<MultitenancyOptions>(Configuration.GetSection("Multitenancy"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();

                try
                {
                    using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
                        .CreateScope())
                    {
                        serviceScope.ServiceProvider.GetService<DataContext>()
                             .Database.Migrate();
                    }
                }
                catch { }
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMultitenancy<AppTenant>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
