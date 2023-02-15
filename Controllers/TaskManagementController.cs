using Microsoft.AspNetCore.Mvc;
using _2.Services;
using Task = _2.Models.Task;
namespace _2.Controllers
{
[ApiController]
[Route("[controller]")]
public class TaskManagementController : ControllerBase
{

    [HttpGet]
    public List<Task> Get()
    {
        return TaskService.Get();
    }
    [HttpGet("{id}")]
    public ActionResult<Task> Get(int id)
    {
        var task = TaskService.Get(id);
        if (task == null)
            return NotFound();
        return task;
    }
    [HttpPost]
    public ActionResult Post(Task task){
        TaskService.Post(task);
        return CreatedAtAction(nameof(Post),new {id = task.id},task);
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id, Task task){
       if(! TaskService.Put(id,task))
            return BadRequest();
         return NoContent();
       
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id){
        if(! TaskService.Delete(id)){
            return NotFound();

        }
        return NoContent();
    }


}}