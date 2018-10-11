using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;

namespace MutitenantMSAL.Helper
{
    public class GraphClientService : IGraphClientService
    {
        private readonly IGraphAuthProvider _authProvider;
        private GraphServiceClient _graphClient;

        public GraphClientService(IGraphAuthProvider authProvider)
        {
            _authProvider = authProvider;
        }

        // Get an authenticated Microsoft Graph Service client.
        public GraphServiceClient GetAuthenticatedClient(string userId, string userTenant)
        {
            _graphClient = new GraphServiceClient(new DelegateAuthenticationProvider(
                async requestMessage =>
                {
                    // Passing tenant ID to the sample auth provider to use as a cache key
                    var accessToken = await _authProvider.GetUserAccessTokenAsync(userId, userTenant);

                    // Append the access token to the request
                    requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // This header identifies the sample in the Microsoft Graph service. If extracting this code for your project please remove.
                    //requestMessage.Headers.Add("SampleID", "aspnetcore-connect-sample");
                }));

            return _graphClient;
        }
    }
    public interface IGraphClientService
    {
        GraphServiceClient GetAuthenticatedClient(string userId, string userTenant);
    }
}
