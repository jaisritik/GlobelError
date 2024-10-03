using GlobelError.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.PathSegments;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace GlobelError.Middleware
{
    public class ExceptionMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Somthing went worng!..{ex}");
                //_logger.LogError(ex.ToString());
                //_logger.LogDebug(ex + "This is Debug Logger");
                //_logger.LogInformation(ex + "This is Information Logger");
                //_logger.LogWarning("Warning Logger");
                //_logger.LogTrace("This is trace Logger");
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                //string logFilePath = $@"D:\coreWebApi\GlobelError\{currentDate}.txt";
                //string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs");
                string logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "logs");
                if(!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
                string file = Path.Combine(logFilePath, $"{currentDate}.txt");
                using (StreamWriter writer = new StreamWriter(file, append: true))
                {
                    writer.WriteLine($"Exception occurred at: {DateTime.Now}");
                    writer.WriteLine($"ErrorlineNo = {ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7)}");
                    writer.WriteLine($"Exception type: {ex.GetType().ToString()}");
                    writer.WriteLine($"Error Message: {ex.Message}");
                    writer.WriteLine($"StackTrace: {ex.StackTrace}");
                    writer.WriteLine("\n------------------------------------------------------------------------------------\n");
                }
                await HandelException(context, ex);
            }
        }

        private static async Task HandelException(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            var errorResponse = new ErrorResponse
            {
                Status = statusCode,
                Message = exception.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode=statusCode;
            await context.Response.WriteAsync(errorResponse.ToString());

        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static void UseExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
