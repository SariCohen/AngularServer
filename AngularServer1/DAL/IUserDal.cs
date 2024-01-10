using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.DAL
{
    public interface IUserDal
    {
        Task<List<User>> AddUserDal(User user);
        public Task<User> AuthenticateDal(UserLogin userLogin);
    }
  
}
