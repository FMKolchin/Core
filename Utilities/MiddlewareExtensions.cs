using _4.Middleware;

namespace _4.Utilities;

public static class MiddlewareExtensions{
       public static IApplicationBuilder UseLogMiddleware(
        this IApplicationBuilder app
    )
    {
        return app.UseMiddleware<LogMiddleware>();
       
    }
}