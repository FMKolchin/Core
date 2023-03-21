using System.Text.Json;
using _4.Interfaces;
using _4.Models;

namespace _4.Services;

public class UserService : IUserService
{
    List<User> Users { get; } = new List<User>();

    private string filePath;
    private IWebHostEnvironment webHost;
    public UserService(IWebHostEnvironment webHost)
    {
        this.webHost = webHost;
        this.filePath = Path.Combine(webHost.ContentRootPath, "Data", "users.json");

        using (var jsonFile = File.OpenText(this.filePath))
        {
            List<User>? temp = JsonSerializer.Deserialize<List<User>>(jsonFile.ReadToEnd(),
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            if (temp != null)
            {
                this.Users = temp;
            }
        }
    }
    public User? Login(User user){
        User? tempUser = Users.FirstOrDefault(u=>u.Password == user.Password && u.UserName == user.UserName);
        return tempUser;
        
    }
    public List<User> Get()
    {
        return Users;
    }
    public User? Get(string id)
    {
        User? user = Users.FirstOrDefault(t => t.Id == id);
        return user;
    }
    public User Post(User user)
    {
        user.Id = Users.Max(t => t.Id) + 1;
        Users.Add(user);
        saveList(Users);
        return user;
    }
    public bool Put(string id, User updateUser)
    {
        if (id != updateUser.Id)
        {
            return false;
        }
        User? user = Users.FirstOrDefault(t => t.Id == id);
        if (user != null)
        {
            user.UserName = updateUser.UserName;
            user.Password = updateUser.Password;
            saveList(Users);
            return true;
        }
        return false;
    }
    public bool Delete(string id)
    {
        User? user = Users.FirstOrDefault(t => t.Id == id);
        if (user == null)
        {
            return false;
        }
        Users.Remove(user);
        
        saveList(Users);
        return true;
    }
    private void saveList(List<User> list)
    {
        File.WriteAllText(filePath, JsonSerializer.Serialize(Users));
    }

 
}