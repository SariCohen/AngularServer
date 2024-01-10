using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.BL
{
    public interface IWinnerService
    {
        public Task<User> AddWinner(int presentId);
        public Task<User> getPresentWinner(int presentId);
        
    }
}

