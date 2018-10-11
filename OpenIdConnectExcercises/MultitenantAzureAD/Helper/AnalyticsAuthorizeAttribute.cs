using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApplication5.Models;

namespace WebApplication5.Helper
{
    public class AnalyticsAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly AppTenant _appTenant;

        public AnalyticsAuthorizeAttribute(AppTenant appTenant)
        {
            _appTenant = appTenant;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var claimsTenant = context.HttpContext.User.Claims.Single(i => i.Type == "http://schemas.microsoft.com/identity/claims/tenantid").Value;

                if(_appTenant.TenantId != claimsTenant)
                {
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
 
                    var response = new { Message = "Unauthorized", Code = (int)System.Net.HttpStatusCode.Unauthorized };

                    context.Result = new JsonResult(response);

                    //context.Result = new ViewResult()
                    //{
                    //    ViewName = "AuthError",
                    //    StatusCode = (int)System.Net.HttpStatusCode.Unauthorized
                    //};
                }
            }
        }
    }
}
