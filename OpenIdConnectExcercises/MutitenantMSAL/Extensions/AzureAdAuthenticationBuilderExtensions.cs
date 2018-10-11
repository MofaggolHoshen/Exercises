using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Identity.Client;
using MutitenantMSAL.Helper;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using MutitenantMSAL.Models;

namespace MutitenantMSAL.Extensions
{
    public static class AzureAdAuthenticationBuilderExtensions
    {
        //public static AuthenticationBuilder AddAzureAd(this AuthenticationBuilder builder)
        //    => builder.AddAzureAd(_ => { });

        public static AuthenticationBuilder AddAzureAd(this AuthenticationBuilder builder, IConfiguration configuration)
        {
            builder.Services.Configure<AzureAdOptions>(options => configuration.Bind("AzureAd", options));
            builder.Services.Configure<MultitenancyOptions>(options => configuration.Bind("Multitenancy", options));
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddSingleton<IConfigureOptions<OpenIdConnectOptions>, ConfigureAzureOptions>();
            builder.AddOpenIdConnect();
            return builder;
        }

        public class ConfigureAzureOptions : IConfigureNamedOptions<OpenIdConnectOptions>
        {
            private readonly AzureAdOptions _azureOptions;
            private readonly MultitenancyOptions _multitenancyOptions;
            private readonly IServiceProvider _serviceProvider;
            private readonly string _baseUrl;

            public AzureAdOptions GetAzureAdOptions() => _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureAdOptions> azureOptions, IOptions<MultitenancyOptions> multitenancyOptions, 
                                         IHttpContextAccessor httpContextAccessor, IServiceProvider serviceProvider)

            {
                _azureOptions = azureOptions.Value;
                _multitenancyOptions = multitenancyOptions.Value;
                _serviceProvider = serviceProvider;
                _baseUrl = httpContextAccessor.HttpContext.Request.GetDisplayUrl();
            }

            public void Configure(string name, OpenIdConnectOptions options)
            {
                options.ClientId = _azureOptions.ClientId;
                options.Authority = $"{_azureOptions.Instance}common/v2.0";
                options.UseTokenLifetime = true;
                //options.CallbackPath = _azureOptions.CallbackPath;//_azureOptions.CallbackPath;
                options.RequireHttpsMetadata = false;
                options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                var allScopes = $"{_azureOptions.Scopes} {_azureOptions.GraphScopes}".Split(new[] { ' ' });
                foreach (var scope in allScopes) { options.Scope.Add(scope); }

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // Ensure that User.Identity.Name is set correctly after login
                    NameClaimType = "name",

                    // Instead of using the default validation (validating against a single issuer value, as we do in line of business apps),
                    // we inject our own multitenant validation logic
                    ValidateIssuer = true,

                    // If the app is meant to be accessed by entire organizations, add your issuer validation logic here.
                    IssuerValidator = (issuer, securityToken, validationParameters) =>
                    {
                        try
                        {
                          
                            //User tenant information
                            var tenant = _multitenancyOptions.Tenants.Single(t => issuer.Contains(t.TenantId));

                            // User claims
                            var token = (securityToken as JwtSecurityToken).Claims.ToList();

                            //User domain name from his unique name
                            var userDomain = token.Single(i => i.Type == "preferred_username").Value.Split('@')[1];

                            //Url from where request is coming 
                            var url = _serviceProvider.GetService<IHttpContextAccessor>().HttpContext.Request.GetDisplayUrl().Split('/')[2]; //ipc.analytics.com
                            //TODO: We can split 'ipc.analytics.com' = ipc

                            //TODO: Need to check requested sub domain is equal to user email domain
                            if (!tenant.Hostnames.Any(i => i == url)) // ipc == userDomain
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
                        context.Response.Redirect("/Home/Error");
                        context.HandleResponse(); // Suppress the exception
                        return Task.CompletedTask;
                    },
                    OnAuthorizationCodeReceived = async (context) =>
                    {
                        try
                        {
                            var code = context.ProtocolMessage.Code;
                            var identifier = context.Principal.FindFirst(OpenIdConnectType.ObjectIdentifier).Value;
                            var memoryCache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                            var graphScopes = _azureOptions.GraphScopes.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            var callbackUrl = _serviceProvider.GetService<IHttpContextAccessor>().HttpContext.Request.GetDisplayUrl();

                            var cca = new ConfidentialClientApplication(
                                _azureOptions.ClientId,
                                callbackUrl,
                                new ClientCredential(_azureOptions.ClientSecret),
                                new SessionTokenCache(identifier, memoryCache).GetCacheInstance(),
                                null);
                            var result = await cca.AcquireTokenByAuthorizationCodeAsync(code, graphScopes);

                            // Check whether the login is from the MSA tenant. 
                            // The sample uses this attribute to disable UI buttons for unsupported operations when the user is logged in with an MSA account.
                            var currentTenantId = context.Principal.FindFirst(OpenIdConnectType.TenantId).Value;
                            if (currentTenantId == "9188040d-6c67-4c5b-b112-36a304b66dad")
                            {
                                // MSA (Microsoft Account) is used to log in
                            }

                            context.HandleCodeRedemption(result.AccessToken, result.IdToken);
                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                    }
                };
            }

            public void Configure(OpenIdConnectOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
