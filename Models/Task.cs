namespace _2.Models;

public class Task
{
    public int id { get; set; }

    public string name { get; set; }

    public bool wasDone { get; set; }

    public Task(int id, string name, bool wasDone){
        this.id = id;
        this.name = name;
        this.wasDone = wasDone;
    }
}