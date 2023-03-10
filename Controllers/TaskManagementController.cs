using Microsoft.AspNetCore.Mvc;
using _4.Interfaces;
using TaskTODO = _4.Models.TaskTODO;
namespace _4.Controllers
{
[ApiController]
[Route("[controller]")]
public class TaskManagementController : ControllerBase
{
    private ITaskService taskService;
    public TaskManagementController(ITaskService taskService){
        this.taskService = taskService;
    }

    [HttpGet]
    public List<TaskTODO> Get()
    {
        return taskService.Get();
    }
    [HttpGet("{id}")]
    public ActionResult<TaskTODO> Get(int id)
    {
        var task = taskService.Get(id);
        if (task == null)
            return NotFound();
        return task;
    }
    [HttpPost]
    public ActionResult Post(TaskTODO task){
        taskService.Post(task);
        return CreatedAtAction(nameof(Post),new {id = task.Id},task);
    }
    [HttpPut("{id}")]
    public ActionResult Put(int id, TaskTODO task){
       if(! taskService.Put(id,task))
            return BadRequest();
         return NoContent();
       
    }
    [HttpDelete("{id}")]
    public ActionResult Delete(int id){
        if(! taskService.Delete(id)){
            return NotFound();

        }
        return NoContent();
    }


}}