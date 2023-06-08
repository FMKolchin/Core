using System.Text.Json;
using _4.Interfaces;
using _4.Models;


namespace _4.Services;

public class TaskSQLService : ITaskService
{
    List<TaskTODO>  Tasks { get; } = new List<TaskTODO>();


    private Context context = new Context();
    public TaskSQLService()
    {
         Tasks = this.context.TaskTODOs.ToList();
        
    }
    public List<TaskTODO> Get()
    {
       
        return Tasks;
    }
    public List<TaskTODO> Get(string token)
    {
         string id = TokenService.DecodeToken(token);
        List<TaskTODO> filteredTasks = Tasks.Where(t=> t.User == id).ToList();
        return filteredTasks;
    }
    public TaskTODO Post(TaskTODO task)
    {
        System.Console.WriteLine("service"+task);
        var temp = Tasks.Select(t=>Int32.Parse(t.Id!));
        task.Id = (temp.Max() + 1).ToString();
        Tasks.Add(task);
        saveList(Tasks);
        return task;
    }
    public bool Put(string id, TaskTODO updateTask)
    {
        if (id != updateTask.Id)
        {
            return false;
        }
        TaskTODO? task = Tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.Description = updateTask.Description;
            task.Status = updateTask.Status;
            saveList(Tasks);
            return true;
        }
        return false;
    }
    public bool Delete(string id,string userId)
    {
        List<TaskTODO> tasks = Tasks.Where(t=>t.User==userId).ToList();
        TaskTODO? task = tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return false;
        }
        Tasks.Remove(task);
        saveList(Tasks);
        return true;
    }
  
    private void saveList(List<TaskTODO> list)
    {
        using (var ctx = this.context)
        {
            ctx.TaskTODOs.RemoveRange(ctx.TaskTODOs.ToList());
            ctx.TaskTODOs.AddRange(list);
            ctx.SaveChanges();
        }
    }

}