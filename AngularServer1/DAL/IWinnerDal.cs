using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.DAL
{
    public interface IWinnerDal
    {
        Task<User> AddWinnerDal(int presentId);
        Task<User> getPresentWinnerDal(int presentId);
    }
}

