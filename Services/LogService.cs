using _4.Interfaces;

namespace _4.Services;

public class LogService : ILogService
{
    private readonly string filePath;

    public LogService(IWebHostEnvironment web){
        this.filePath = Path.Combine(web.ContentRootPath,"Logs","LogControllers.log");

    }

    public void Log(LogLevel level, string message)
    {
          using (var sr = new StreamWriter(filePath, true))
        {
            sr.WriteLine(
                $"{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:ms")} [{level}] {message}");
        }
    }
}