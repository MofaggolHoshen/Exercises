using FiltersSample.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FiltersSample.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        /*
         * I can not DI in attribute, because 
         * 
         * [ValidateModel(here i need to provide service)]
         * 
         * private readonly IUserService _user;
         *
         * public ValidateModelAttribute(IUserService user)
         * {
         *   _user = user;
         * }
         * 
         * For injecting i can user ServiceFilter 
         */

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
