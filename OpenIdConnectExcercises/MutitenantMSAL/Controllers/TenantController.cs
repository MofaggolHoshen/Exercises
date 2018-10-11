using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MutitenantMSAL.Helper;
using MutitenantMSAL.Models;
using Newtonsoft.Json;

namespace MutitenantMSAL.Controllers
{
    [Authorize]
    public class TenantController : Controller
    {
        private readonly IGraphClientService _graphSdkHelper;
        private readonly DataContext _context;

        public TenantController(DataContext context, IGraphClientService graphSdkHelper)
        {
            _context = context;
            _graphSdkHelper = graphSdkHelper;

        }

        public async Task<IActionResult> GetUser()
        {

            if (User.Identity.IsAuthenticated)
            {
                // Get users's email.
                var email = User.FindFirst("preferred_username")?.Value;

                // Get user's id for token cache.
                var userId = User.FindFirst(OpenIdConnectType.ObjectIdentifier)?.Value;
                var tenant = User.FindFirst(OpenIdConnectType.TenantId)?.Value;

                // Initialize the GraphServiceClient.
                var graphClient = _graphSdkHelper.GetAuthenticatedClient(userId, tenant);

                var userinfo = await GetUserJson(graphClient, email, HttpContext);

                var longedinUserInformation = JsonConvert.DeserializeObject<Microsoft.Graph.User>(userinfo);

                if(!_context.Users.Any(u=> u.Id.ToString() == userId))
                {
                    _context.Users.Add(new Models.User()
                    {
                        Id = Guid.Parse(userId),
                        FirstName = longedinUserInformation.GivenName,
                        LastName = longedinUserInformation.Surname,
                        TenantId = Guid.Parse(tenant)
                    });

                    _context.SaveChanges();
                }

                ViewBag.UserInfo = userinfo;

                var Administrator = User.IsInRole("Administrator");
                var FrontOfficer = User.IsInRole("FrontOfficer");
            }

            return View();

        }


        public async Task<string> GetUserJson(GraphServiceClient graphClient, string email, HttpContext httpContext)
        {
            if (email == null) return JsonConvert.SerializeObject(new { Message = "Email address cannot be null." }, Formatting.Indented);

            try
            {
                // Load user profile.
                var user = await graphClient.Users[email].Request().GetAsync();
                return JsonConvert.SerializeObject(user, Formatting.Indented);
            }
            catch (ServiceException e)
            {
                switch (e.Error.Code)
                {
                    case "Request_ResourceNotFound":
                    case "ResourceNotFound":
                    case "ErrorItemNotFound":
                    case "itemNotFound":
                        return JsonConvert.SerializeObject(new { Message = $"User '{email}' was not found." }, Formatting.Indented);
                    case "ErrorInvalidUser":
                        return JsonConvert.SerializeObject(new { Message = $"The requested user '{email}' is invalid." }, Formatting.Indented);
                    case "AuthenticationFailure":
                        return JsonConvert.SerializeObject(new { e.Error.Message }, Formatting.Indented);
                    case "TokenNotFound":
                        await httpContext.ChallengeAsync();
                        return JsonConvert.SerializeObject(new { e.Error.Message }, Formatting.Indented);
                    default:
                        return JsonConvert.SerializeObject(new { Message = "An unknown error has occurred." }, Formatting.Indented);
                }
            }
        }
    }
}
