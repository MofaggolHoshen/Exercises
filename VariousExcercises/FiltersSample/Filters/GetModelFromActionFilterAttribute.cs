using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FiltersSample.Domain;
using FiltersSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FiltersSample.Filters
{
    public class GetModelFromActionFilterAttribute : IActionFilter
    {
        private readonly IOrganizationalCheckService organizationalCheckService;
        private readonly ILogger<GetModelFromActionFilterAttribute> logger;

        public GetModelFromActionFilterAttribute(IOrganizationalCheckService organizationalCheckService, ILogger<GetModelFromActionFilterAttribute> logger)
        {
            this.organizationalCheckService = organizationalCheckService;
            this.logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var model = ((ViewResult) context.Result).Model;
            logger.LogInformation("Hi, I am in Action Filter.");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            logger.LogInformation("Hi, I am in Action Filter.");
        }
    }
}
