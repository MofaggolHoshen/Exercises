using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MutitenantMSAL.Models;

namespace MutitenantMSAL.Helper
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
            //if (context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    var claimsTenant = context.HttpContext.User.Claims.Single(i => i.Type == "http://schemas.microsoft.com/identity/claims/tenantid").Value;

            //    if (_appTenant.TenantId != claimsTenant)
            //    {
                    context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;

                    var response = new { Message = "Unauthorized", Code = (int)System.Net.HttpStatusCode.Unauthorized };

            context.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;

            var viewResult = new ViewResult();
            viewResult.ViewName = "Error";
            viewResult.StatusCode = (int)System.Net.HttpStatusCode.Unauthorized;
            viewResult.ViewData = new ViewDataDictionary(new Microsoft.AspNetCore.Mvc.ModelBinding.EmptyModelMetadataProvider(), new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
            {
                Model = new ErrorViewModel { Message = "Unauthorized", Code = (int)System.Net.HttpStatusCode.Unauthorized, RequestId = Activity.Current?.Id ?? context.HttpContext.TraceIdentifier }
            };

            context.Result = viewResult;

            //    }
            //}
        }

    }
}
