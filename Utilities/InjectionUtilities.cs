
using _4.Interfaces;
using _4.Services;
namespace _4.Utilities
{
public static class InjectionUtilities{
    public static void AddTasks(this IServiceCollection service){
            service.AddSingleton<ITaskService, TaskService>();
    }
     public static void AddUsers(this IServiceCollection service){
            service.AddSingleton<IUserService, UserService>();
    }

}
}