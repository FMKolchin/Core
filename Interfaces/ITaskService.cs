using _4.Models;


namespace _4.Interfaces {


    public interface ITaskService
    {
        public List<TaskTODO> Get();
        public List<TaskTODO> Get(string token);
        public TaskTODO Post(TaskTODO task);
        public bool Put(string id, TaskTODO updateTask);
        public bool Delete(string id);

    }

}