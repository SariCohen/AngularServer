using AngularServer1.Modal;

namespace AngularServer1.DAL
{
    public interface IDonorDal
    {
        Task<List<Donor>> GetAllDonors();
        Task<List<Donor>> AddDonorDal(Donor donor);
        Task<List<Donor>> DeleteDonorDal(string donorId);
        Task<List<Donor>> UpdateDonorDal(string donorId,Donor donorUp);
        Task<List<Donor>> GetDonorByNameDal(string name);
        Task<List<Donor>> GetDonorByEmailDal(string email);
        Task<List<Donor>> GetDonorByPresentDal(string present);
       
        
    }
}
