using AngularServer1.Modal;

namespace AngularServer1.BL
{
    public interface IDonorService
    {
        public Task<List<Donor>> GetDonors();
        public Task<List<Donor>> AddDonor(Donor donor);
        public Task<List<Donor>> DeleteDonor(string donorId);
        public Task<List<Donor>> UpdateDonor(string donorId, Donor donorUp);
        public Task<List<Donor>> GetDonorByName(string name);
        public Task<List<Donor>> GetDonorByEmail(string email);
        public Task<List<Donor>> GetDonorByPresent(string present);

    }
}
