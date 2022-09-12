using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace FiltersSample.Filters
{
    /*
     * It a sample of Service Filter
     * 
     * A filter that surrounds execution of action results successfully returned from
     * an action.
     * 
     * In Startup.cs
     *      services.AddScoped<ServiceFilterSample>();
     * 
     * In Controller or Action 
     *      [ServiceFilter(typeof(ServiceFilterSample))]
     * 
     */
    public class ServiceFilterSample : IResultFilter
    {
        private ILogger _logger;
        public ServiceFilterSample(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ServiceFilterSample>();
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            var headerName = "OnResultExecuting";
            context.HttpContext.Response.Headers.Add(
                headerName, new string[] { "ResultExecutingSuccessfully" });
            _logger.LogInformation($"Header added: {headerName}");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Can't add to headers here because response has already begun.
        }
    }
}