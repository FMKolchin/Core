using _2.Interfaces;
using _2.Models;


namespace _2.Services;


public class TaskService : ITaskService{

private List<TaskTODO> tasks = new List<TaskTODO>(){
    new TaskTODO(1,"לעשות שב בCORE",false),
    new TaskTODO(2,"ללמוד למבחן בJAVA",false),
    new TaskTODO(3,"להשיג חומרים למבחנים",false),
    new TaskTODO(4,"לאכול מלוה מלכה",false),
    new TaskTODO(5,"ללכת לחוג התעמלות",false),
    new TaskTODO(6,"ללוות את שפרה לתחנה",true)
};
// private ITaskService taskService;

// public TaskService(ITaskService service){
//     this.taskService = service;
// }

public List<TaskTODO> Get()
{
    return tasks;
}
public TaskTODO? Get(int id){
    TaskTODO? task =tasks.FirstOrDefault(t => t.id == id); 
    return task ;
}
public TaskTODO Post(TaskTODO task){
 task.id = tasks.Max(t=>t.id)+1;
 tasks.Add(task);
 return task;
}
public bool Put(int id,TaskTODO updateTask){
    if(id!=updateTask.id){
        return false;
    }
   TaskTODO? task = tasks.FirstOrDefault(t => t.id == id);
    if(task!=null){
        task.name = updateTask.name;
        task.wasDone = updateTask.wasDone;
        return true;
    }
    return false;
}
public bool Delete(int id){
    TaskTODO? task =  tasks.FirstOrDefault(t => t.id == id);
    if(task==null){
        return false;
    }
    tasks.Remove(task);
    return true;
}



}