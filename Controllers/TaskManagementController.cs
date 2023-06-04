using Microsoft.AspNetCore.Mvc;
using _4.Interfaces;
using _4.Models;
using Microsoft.AspNetCore.Authorization;
using _4.Services;

namespace _4.Controllers
{
[ApiController]
[Route("[controller]")]
[Authorize(Policy = "Agent")]
public class TaskManagementController : ControllerBase
{
    private ITaskService taskService;
    public TaskManagementController(ITaskService taskService){
        this.taskService = taskService;
    }

    [HttpGet]
   [Authorize(Policy = "Agent")]
    public ActionResult<List<TaskTODO>> Get()
    {
        System.Console.WriteLine("in get!!");
             string? token = HttpContext.Request.Headers["Authorization"]; 
        var task = taskService.Get(token!);
        if (task == null)
            return NotFound();
        return task;
    }
    [HttpGet("{id}")]
   // [Authorize(Policy = "Agent")]
    public ActionResult<TaskTODO> Get(string id)
    {
               string? token = HttpContext.Request.Headers["Authorization"]; 
        var tasks = taskService.Get(token!);
        var task = tasks.FirstOrDefault(t=>t.Id == id);
        if (task == null)
            return NotFound();
        return task;
    }
    
     //[Authorize(Policy = "Agent")]
    [HttpPost]
    public ActionResult Post(TaskTODO task){
        System.Console.WriteLine("in controller post");
        System.Console.WriteLine("controller"+task.ToString());
        string userId = TokenService.DecodeToken(HttpContext.Request.Headers["Authorization"]!);
        System.Console.WriteLine(userId);
        task.User = userId;
        taskService.Post(task);
        return CreatedAtAction(nameof(Post),new {id = task.Id},task);
    }
    [HttpPut("{id}")]
    //[Authorize(Policy = "Agent")]
    public ActionResult Put(string id, TaskTODO task){
        string userId = TokenService.DecodeToken(HttpContext.Request.Headers["Authorization"]!);
       if(! taskService.Put(id,task))
            return BadRequest();
         return NoContent();
       
    }
    [HttpDelete("{id}")]
     //[Authorize(Policy = "Agent")]
    public ActionResult Delete(string id){
         string userId = TokenService.DecodeToken(HttpContext.Request.Headers["Authorization"]!);
        if(! taskService.Delete(id,userId)){
            return NotFound();

        }
        return NoContent();
    }


}}