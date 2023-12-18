using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;
using Common.Enums;
using Common.Exceptions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Common.Dto;

namespace Sub.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, logger, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, ILogger<ErrorHandlingMiddleware> logger, Exception ex)
        {
            var errorResponse = new ErrorResponseDto();
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            errorResponse.ErrorCode = ErrorCode.UNKNOWN;
            errorResponse.Message = ex.Message;
            if (ex is SUBException)
            {
                var exception = (ex as SUBException);
                code = exception.StatusCode;
                errorResponse.Message = exception.Message;
                errorResponse.ErrorCode = exception.ErrorCode;
            }

            logger.LogError(errorResponse.Message);
            var result = JsonConvert.SerializeObject(new
            {
                Message = errorResponse.Message,
                ErrorCode = errorResponse.ErrorCode.ToString()
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }

}
