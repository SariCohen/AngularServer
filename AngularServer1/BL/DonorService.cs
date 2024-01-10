using AngularServer1.DAL;
using AngularServer1.Modal;

namespace AngularServer1.BL
{
    public class DonorService: IDonorService
    {
        private readonly IDonorDal _donorDal;

        public DonorService(IDonorDal donorDal)
        {
            this._donorDal=donorDal?? throw new ArgumentNullException(nameof(donorDal));
        }


        public async Task<List<Donor>> GetDonors()
        {
            return await _donorDal.GetAllDonors();
        }

        public async Task<List<Donor>> AddDonor(Donor donor)
        {
            return await _donorDal.AddDonorDal(donor);
        }
        public async Task<List<Donor>> DeleteDonor(string donorId)
        {
            return await _donorDal.DeleteDonorDal(donorId);
        }
        public async Task<List<Donor>> UpdateDonor(string donorId, Donor donorUp)
        {
            return await _donorDal.UpdateDonorDal(donorId, donorUp);
        }
        public async Task<List<Donor>> GetDonorByName(string name)
        {
            return await _donorDal.GetDonorByNameDal(name);
        }
        public async Task<List<Donor>> GetDonorByEmail(string email)
        {
            return await _donorDal.GetDonorByEmailDal(email);
        }
        public async Task<List<Donor>> GetDonorByPresent(string present)
        {
            return await _donorDal.GetDonorByPresentDal(present);
        }

    }
}
