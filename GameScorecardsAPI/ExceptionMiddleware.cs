using GameScorecardsAPI.Exceptions;
using GameScorecardsModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace GameScorecardsAPI
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate m_Next;
        private readonly ILogger m_Logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            m_Next = next;
            m_Logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            if (httpContext.Request.Headers.TryGetValue("X-RequestId", out var requestId))
            {
                httpContext.Response.Headers["X-RequestId"] = requestId;
            }

            try
            {
                await m_Next(httpContext);
            }
            catch (Exception ex)
            {
                m_Logger.LogError($"Something went wrong: {ex.Message}");
                await HandleGlobalExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status = HttpStatusCode.InternalServerError;
            if (exception is HttpStatusCodeException statusCodeException)
            {
                status = statusCodeException.StatusCode;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            var response = new RestResponse
            {
                StatusCode = status,
                Message = exception.Message,
            };

            var content = JsonConvert.SerializeObject(response);
            return context.Response.WriteAsync(content);
        }

    }
}
