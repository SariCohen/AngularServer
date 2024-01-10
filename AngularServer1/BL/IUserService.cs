using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.BL
{
    public interface IUserService
    {
        public Task<List<User>> AddUser(User user);
        //public User ValidateToken(string token);
        public string Generate(User user);
        public Task<User> Authenticate(UserLogin userLogin);  



    }
}
