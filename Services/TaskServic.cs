using _2.Models;
using Task = _2.Models.Task;

namespace _2.Services;


public class TaskService{

private static List<Task> tasks = new List<Task>(){
    new Task(1,"לעשות שב בCORE",false),
    new Task(2,"ללמוד למבחן בJAVA",false),
    new Task(3,"להשיג חומרים למבחנים",false),
    new Task(4,"לאכול מלוה מלכה",false),
    new Task(5,"ללכת לחוג התעמלות",false),
    new Task(6,"ללוות את שפרה לתחנה",true)
};

public static List<Task> Get()
{
    return tasks;
}
public static Task Get(int id){
    return tasks.FirstOrDefault(t => t.id == id);
}
public static Task Post(Task task){
 task.id = tasks.Max(t=>t.id)+1;
 tasks.Add(task);
 return task;
}
public static bool Put(int id,Task updateTask){
    if(id!=updateTask.id){
        return false;
    }
   Task task = tasks.FirstOrDefault(t => t.id == id);
    if(task!=null){
        task.name = updateTask.name;
        task.wasDone = updateTask.wasDone;
        return true;
    }
    return false;
}
public static bool Delete(int id){
    Task task = tasks.FirstOrDefault(t => t.id == id);
    if(task==null){
        return false;
    }
    tasks.Remove(task);
    return true;
}



}