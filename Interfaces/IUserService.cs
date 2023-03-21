using _4.Models;


namespace _4.Interfaces {


    public interface IUserService
    {
        public User? Login(User user);
        public List<User> Get();
        public User? Get(string id);
        public User Post(User user);
        public bool Put(string id, User updateUser);
        public bool Delete(string id);

    }

}