namespace _2.Models;

public class TaskTODO
{
    public int id { get; set; }

    public string name { get; set; }

    public bool wasDone { get; set; }

    public TaskTODO(int id, string name, bool wasDone){
        this.id = id;
        this.name = name;
        this.wasDone = wasDone;
    }
}