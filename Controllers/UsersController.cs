using Microsoft.AspNetCore.Mvc;
using _4.Interfaces;
using User = _4.Models.User;
using System.Security.Claims;
using _4.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
//using System.IdentityModel.Tokens.Jwt;

namespace _4.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService userService;
    public UsersController(IUserService userService){
        this.userService = userService;
    }
    [HttpPost]
    [Route("[action]")]
        public ActionResult<String> Login(User User)
        {
            User? authUser = userService.Login(User);
            if (authUser==null)
            {
                return Unauthorized();
            }
            

            var claims = new List<Claim>
            {
                new Claim("UserName",authUser.UserName!),
                new Claim("Id",authUser.Id!),
                new Claim("Classification", authUser.Classification!)
            };

            var token = TokenService.GetToken(claims);
            return new OkObjectResult(
                new {token=TokenService.WriteToken(token),
                user = authUser.Id,
                classification = authUser.Classification}
                );
        }



    [HttpGet]
    [Authorize(Policy = "Admin")]
    public List<User> Get()
    {
        return userService.Get();
    }
    [HttpGet]
    [Route("GetUser")]
    [Authorize(Policy = "Agent")]
    public ActionResult<User> GetUser()
    {
        string? token = HttpContext.Request.Headers["Authorization"]; 
        string id = TokenService.DecodeToken(token!);
        var user = userService.Get(id);
        if (user == null)
            return NotFound();
        return user;
    }
    [Authorize(Policy = "Admin")]
    [HttpPost]
    public ActionResult Post(User user){
        System.Console.WriteLine(user.Id);
        userService.Post(user);
        return CreatedAtAction(nameof(Post),new {id = user.Id},user);
    }
    [HttpPut("{id}")]
     [Authorize(Policy = "Agent")]
    public ActionResult Put(string id, User user){
       if(! userService.Put(id,user))
            return BadRequest();
         return NoContent();
       
    }
    [HttpDelete("{id}")]
     [Authorize(Policy = "Agent")]
    public ActionResult Delete(string id){
        // string? token = HttpContext.Request.Headers["Authorization"]; 
        // string id = TokenService.DecodeToken(token!);
        if(! userService.Delete(id)){
            return NotFound();

        }
        return NoContent();
    }




}