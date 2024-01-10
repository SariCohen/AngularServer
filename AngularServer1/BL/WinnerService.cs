using AngularServer1.DAL;
using AngularServer1.Modal;
using AngularServer1.Models;

namespace AngularServer1.BL
{
    public class WinnerService: IWinnerService
    {
        private readonly IWinnerDal _winnerDal;

        public WinnerService(IWinnerDal winnerDal)
        {
            this._winnerDal = winnerDal ?? throw new ArgumentNullException(nameof(winnerDal));

        }

        public async Task<User> AddWinner(int presentId)
        {
            return await _winnerDal.AddWinnerDal(presentId);
        }

        public async Task<User> getPresentWinner(int presentId)
        {
            return await _winnerDal.getPresentWinnerDal(presentId);
        }
    }
}
