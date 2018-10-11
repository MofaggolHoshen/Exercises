using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;

namespace MutitenantMSAL.Helper
{
    public class GraphAuthProvider : IGraphAuthProvider
    {
        private readonly IMemoryCache _memoryCache;
        private TokenCache _tokenCache;

        private readonly string _appId;
        private readonly ClientCredential _credential;
        private readonly string[] _scopes;
        private readonly string _redirectUri;
        

        public GraphAuthProvider(IMemoryCache memoryCache, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            var azureOptions = new AzureAdOptions();
            configuration.Bind("AzureAd", azureOptions);

            _appId = azureOptions.ClientId;
            _credential = new ClientCredential(azureOptions.ClientSecret);
            _scopes = azureOptions.GraphScopes.Split(new[] { ' ' });

            var requestedUri = httpContextAccessor.HttpContext.Request;

            _redirectUri = $"{requestedUri.Scheme}://{requestedUri.Host.Value}{azureOptions.CallbackPath}";//azureOptions.BaseUrl + azureOptions.CallbackPath;

            _memoryCache = memoryCache;
        }
        public async Task<string> GetUserAccessTokenAsync(string userId, string userTenant)
        {
            _tokenCache = new SessionTokenCache(userId, _memoryCache).GetCacheInstance();

            var confidentialClientApplication = new ConfidentialClientApplication(_appId, _redirectUri, _credential, _tokenCache, null);

            //var account = await confidentialClientApplication.GetAccountsAsync("446985bc-8939-42d8-8fbc-962f84527b57.99360fd3-03fe-4f03-9291-fe0c7db80be3");

             var account = await confidentialClientApplication.GetAccountAsync($"{userId}.{userTenant}");

            //var c = account.First();

            var result = await confidentialClientApplication.AcquireTokenSilentAsync(_scopes, account);

            return result.AccessToken;

        }
    }

    public interface IGraphAuthProvider
    {
        Task<string> GetUserAccessTokenAsync(string userId, string userTenant);
    }
}
