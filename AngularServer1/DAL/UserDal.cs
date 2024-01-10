using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace AngularServer1.DAL
{
    public class UserDal:IUserDal
    {
        private readonly ChiniesSaleContext _ChiniesSaleContext;

        public UserDal(ChiniesSaleContext chiniesSaleContext)
        {
            this._ChiniesSaleContext = chiniesSaleContext ?? throw new ArgumentNullException(nameof(chiniesSaleContext));
        }

        public async  Task<List<User>> AddUserDal(User user)
        {
            await _ChiniesSaleContext.Users.AddAsync(user);
            await _ChiniesSaleContext.SaveChangesAsync();
           var a =await _ChiniesSaleContext.Users.ToListAsync();
            return a;
        }

        public async Task<User> AuthenticateDal(UserLogin userLogin)
        {
            var currentUser = _ChiniesSaleContext.Users.FirstOrDefault(o => o.UserName.ToLower() ==
            userLogin.UserName.ToLower() && o.UserPassword == userLogin.UserPassword);
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }



    }
}
