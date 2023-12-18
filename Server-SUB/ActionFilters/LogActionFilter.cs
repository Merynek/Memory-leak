using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;

namespace Sub.Api.ActionFilters
{
    public class LogActionFilter : IActionFilter
    {
        private readonly ILogger _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Log("[After]", context.RouteData);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            Log("[Before]", context.RouteData);
        }        

        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            _logger.LogInformation(String.Format("{0} Controller: {1} Action: {2}", methodName, controllerName, actionName));
        }
    }
}