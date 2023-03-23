using System.Diagnostics;
using _4.Interfaces;
namespace _4.Middleware;

public class LogMiddleware 
{
    private readonly ILogService logger;
    private readonly RequestDelegate next;
     public LogMiddleware (RequestDelegate next, ILogService logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext ctx)
    {
        var sw = new Stopwatch();
        sw.Start();
        await next(ctx);
        sw.Stop();
        logger.Log(LogLevel.Debug,
            $" {ctx.Request.Path}: {sw.ElapsedMilliseconds}ms.");
    }
}