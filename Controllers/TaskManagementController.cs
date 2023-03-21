using Microsoft.AspNetCore.Mvc;
using _4.Interfaces;
using _4.Models;
using Microsoft.AspNetCore.Authorization;

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
    [Authorize(Policy = "Admin")]
    public List<TaskTODO> Get()
    {
        return taskService.Get();
    }
    [Route("GetUser")]
    [HttpGet]
    [Authorize(Policy = "Agent")]
    public ActionResult<List<TaskTODO>> GetUser()
    {
        string? token = HttpContext.Request.Headers["Authorization"]; 
        var task = taskService.Get(token!);
        if (task == null)
            return NotFound();
        return task;
    }
    [HttpPost]
     [Authorize(Policy = "Agent")]
    public ActionResult Post(TaskTODO task){
        taskService.Post(task);
        return CreatedAtAction(nameof(Post),new {id = task.Id},task);
    }
    [HttpPut("{id}")]
     [Authorize(Policy = "Agent")]
    public ActionResult Put(string id, TaskTODO task){
       if(! taskService.Put(id,task))
            return BadRequest();
         return NoContent();
       
    }
    [HttpDelete("{id}")]
     [Authorize(Policy = "Agent")]
    public ActionResult Delete(string id){
        if(! taskService.Delete(id)){
            return NotFound();

        }
        return NoContent();
    }


}}