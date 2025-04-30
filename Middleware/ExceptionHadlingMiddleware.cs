using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CollegeApp.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHadlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHadlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);

            }
            catch (Exception ex)
            {
            }
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHadlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHadlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHadlingMiddleware>();
        }
    }
}
