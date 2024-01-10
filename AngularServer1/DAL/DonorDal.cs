using AngularServer1.Modal;

using AngularServer1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Security.Cryptography;
using System.Xml.Linq;
namespace AngularServer1.DAL
{
    public class DonorDal: IDonorDal
    {
        private readonly ChiniesSaleContext _ChiniesSaleContext;

        public DonorDal(ChiniesSaleContext chiniesSaleContext)
        {
            this._ChiniesSaleContext = chiniesSaleContext ?? throw new ArgumentNullException(nameof(chiniesSaleContext));
        }
        public async Task<List<Donor>> GetAllDonors()
        {
            var donors = await _ChiniesSaleContext.Donors.ToListAsync();
            var donorsWithGifts = new List<Donor>();

            foreach (var donor in donors)
            {
                var gifts = _ChiniesSaleContext.Donations
                    .Where(d => d.Donor.DonorName == donor.DonorName)
                    .Select(d => d.Present.Name)
                    .ToList();

                donor.Donations = gifts;

                donorsWithGifts.Add(donor);
            }
            return donorsWithGifts;

        }

      public async  Task<List<Donor>> AddDonorDal(Donor donor)
        {
            if (donor.DonorName == null || donor.DonorEmail == null || donor.DonorName == null || donor.DonorPhone == null
                || donor.ImageUrl == null || donor.DonationType == null)
            {
                return null;
            }
            else
            {
                await _ChiniesSaleContext.Donors.AddAsync(donor);
                await _ChiniesSaleContext.SaveChangesAsync();
                return await GetAllDonors();
            }

        }
        //On Delete cascad
        public async Task<List<Donor>> DeleteDonorDal(string donorId)
        {
            if(donorId != null) {
                Donor deleteDonor = await _ChiniesSaleContext.Donors.FindAsync(donorId);
                if (deleteDonor != null)
                {
                    _ChiniesSaleContext.Donors.Remove(deleteDonor);
                    await _ChiniesSaleContext.SaveChangesAsync();

                }


                return  await GetAllDonors();
            }
            return null;
           
        }
        public async Task<List<Donor>> UpdateDonorDal(string donorId, Donor donorUp)
        {
            if (donorUp != null)
            {
                Donor d = await _ChiniesSaleContext.Donors.FindAsync(donorId);

                if (d != null)
                {
                    d.DonorEmail = donorUp.DonorEmail;
                    d.DonorPhone = donorUp.DonorPhone;
                    d.DonorName = donorUp.DonorName;
                    d.DonationType = donorUp.DonationType;
                    d.Donations = donorUp.Donations;
                    await _ChiniesSaleContext.SaveChangesAsync();
                }
                return await _ChiniesSaleContext.Donors.ToListAsync();
            }
            return null;
        }


      public async  Task<List<Donor>> GetDonorByNameDal(string name)
        {
            if (name != "undefined" || name != "") {
                var d = await _ChiniesSaleContext.Donors
            .Where(d => d.DonorName == name).ToListAsync();
                return d;
            }
            return await GetAllDonors();
          
        }
        public async Task<List<Donor>> GetDonorByEmailDal(string email)
        {
if (email != "undefined" || email!="") {
                var d = await _ChiniesSaleContext.Donors
                .Where(d => d.DonorEmail == email).ToListAsync();
                return d;
            }
return await GetAllDonors();
          
        }
        public async Task<List<Donor>> GetDonorByPresentDal(string present)
        {
            if (present != "undefined" || present!="") {
                var donors = await _ChiniesSaleContext.Donations
                  .Where(d => d.Present.Name == present)
                  .Select(d => d.Donor)
                  .ToListAsync();
                return donors;
            }
            return await GetAllDonors();
          
        }


    }
    }

