using _2.Models;


namespace _2.Interfaces {


    public interface ITaskService
    {



        public List<TaskTODO> Get();
        public TaskTODO? Get(int id);
        public TaskTODO Post(TaskTODO task);
        public bool Put(int id, TaskTODO updateTask);
        public bool Delete(int id);

    }

}