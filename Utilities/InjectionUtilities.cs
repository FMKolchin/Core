
using _2.Interfaces;
using _2.Services;
namespace _2.Utilities
{
public static class InjectionUtilities{
    public static void AddTasks(this IServiceCollection service){
            service.AddSingleton<ITaskService, TaskService>();
    }
}
}