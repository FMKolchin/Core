using System.Text.Json;
using _4.Interfaces;
using _4.Models;


namespace _4.Services;


public class TaskService : ITaskService{

List<TaskTODO> Tasks{get;} = new List<TaskTODO>();

private string filePath;
private IWebHostEnvironment webHost;
public TaskService(IWebHostEnvironment webHost){
    this.webHost = webHost;
    this.filePath = Path.Combine(webHost.ContentRootPath,"Data","taskList.json");

     using (var jsonFile = File.OpenText(this.filePath))
            {
                List<TaskTODO>? temp = JsonSerializer.Deserialize<List<TaskTODO>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                if(temp != null){
                    this.Tasks = temp;
                }


                
               
               
            }
           

   
}



public List<TaskTODO> Get()
{
    return Tasks;
}
public TaskTODO? Get(int id){
    TaskTODO? task =Tasks.FirstOrDefault(t => t.Id == id); 
    return task ;
}
public TaskTODO Post(TaskTODO task){
 task.Id = Tasks.Max(t=>t.Id)+1;
 Tasks.Add(task);
 saveList(Tasks);
 return task;
}
public bool Put(int id,TaskTODO updateTask){
    if(id!=updateTask.Id){
        return false;
    }
   TaskTODO? task = Tasks.FirstOrDefault(t => t.Id == id);
    if(task!=null){
        task.Description = updateTask.Description;
        task.Status = updateTask.Status;
        saveList(Tasks);
        return true;
    }
    return false;
}
public bool Delete(int id){
    TaskTODO? task =  Tasks.FirstOrDefault(t => t.Id == id);
    if(task==null){
        return false;
    }
    Tasks.Remove(task);
    saveList(Tasks);
    return true;
}

private void saveList(List<TaskTODO> list){
    File.WriteAllText(filePath, JsonSerializer.Serialize(Tasks));
}



}