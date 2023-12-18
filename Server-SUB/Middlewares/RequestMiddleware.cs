using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using Common.Enums;
using Common.Exceptions;
using Common.Singletons;

namespace Sub.Middlewares
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IAppCache appCache)
        {
            var user = httpContext.User;
            if (user != null && user.Identity.IsAuthenticated)
            {
                var userId = user.Identity.Name;
                if (appCache.isBanned(Int32.Parse(userId)))
                {
                    throw new UnauthorizedSUBException(ErrorCode.BANNED, "User is banned");
                }
                await _next(httpContext);
            }
            else
            {
                await _next(httpContext);
            }
        }
    }

    public static class RequestMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestFilter(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestMiddleware>();
        }
    }

}
