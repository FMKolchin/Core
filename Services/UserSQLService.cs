using System.Text.Json;
using _4.Interfaces;
using _4.Models;

namespace _4.Services;

public class UserSQLService : IUserService
{
    List<User> Users { get; } = new List<User>();

    private Context context = new Context();
    public UserSQLService()
    {


        if (context.Users != null)
        {
            this.Users = context.Users.ToList();
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
        var list = Users.Select(t=>Int32.Parse(t.Id!));
        System.Console.WriteLine(list.Max());
        System.Console.WriteLine(list);
        user.Id = (list.Max() + 1).ToString();
        System.Console.WriteLine(user.Id);
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
            user.Classification = updateUser.Classification;
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
        using(var ctx = this.context)
        {
            ctx.Users.RemoveRange(ctx.Users.ToList());
            ctx.Users.AddRange(list);
            ctx.SaveChanges();
        }
    }

 
}