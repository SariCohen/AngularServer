using ActiveUp.Net.Dns;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Security.Cryptography;
using System.Xml.Linq;

namespace AngularServer1.DAL
{
    public class PresentDal : IPresentDal
    {
        private readonly ChiniesSaleContext _ChiniesSaleContext;

        public PresentDal(ChiniesSaleContext chiniesSaleContext)
        {
            this._ChiniesSaleContext = chiniesSaleContext ?? throw new ArgumentNullException(nameof(chiniesSaleContext));
        }

        public async Task<List<present>> GetAllPresent()
        {
            var gifts = await _ChiniesSaleContext.Presents.ToListAsync();
            var giftsWithDonors = new List<present>();

            foreach (var gift in gifts)
            {
                var donors = _ChiniesSaleContext.Donations
                    .Where(d => d.Present.PresentId == gift.PresentId)
                    .Select(d => d.Donor.DonorName)
                    .ToList();

                gift.Donors = donors;
                giftsWithDonors.Add(gift);
            }

            return giftsWithDonors;
        }

        public async Task<List<present>> AddPresentDal(present present)
        {
            if (present.ImagUrl == null || present.Category == null || present.Description == null ||
                present.Donors.Count==0 || present.Name == null || present.Price == null)
                return null;
            else
            {
                await _ChiniesSaleContext.Presents.AddAsync(present);
                await _ChiniesSaleContext.SaveChangesAsync();

                int len = present.Donors.Count;
                var donors = new List<Donor>();
                for (int i = 0; i < len; i++)
                {
                    var donor = await _ChiniesSaleContext.Donors
                     .Where(d => d.DonorName == present.Donors[i])
                     .ToListAsync();
                    donors.AddRange(donor);

                }
                //DTO
                for (int i = 0; i < donors.Count; i++)
                {
                    var donation = new Donation
                    {
                        Donor = donors[i],
                        Present = present
                    };

                    await _ChiniesSaleContext.Donations.AddAsync(donation);
                    await _ChiniesSaleContext.SaveChangesAsync();

                }

                return await GetAllPresent();
            }
        }
        //public async Task<List<present>> DeletePresentDal(int presentId)
        //{
        //    if (presentId == null)
        //    {
        //        return null;
        //    }
        //    else
        //    {
        //        present deletePresent = await _ChiniesSaleContext.Presents.FindAsync(presentId);
        //        if (deletePresent != null)
        //        {
        //            _ChiniesSaleContext.Presents.Remove(deletePresent);
        //            await _ChiniesSaleContext.SaveChangesAsync();
        //        }

        //        return await GetAllPresent();
        ////    }
        //}
        public async Task<List<present>> DeletePresentDal(int presentId)
        {
            if (presentId == null)
            {
                return null;
            }
            else
            {
         
                bool isPresentUsedInOrders = await _ChiniesSaleContext.Orders.AnyAsync(o => o.presentId == presentId && (o.status == "עגלה" || o.status == "שולם"));

                if (!isPresentUsedInOrders)
                {
                    present deletePresent = await _ChiniesSaleContext.Presents.FindAsync(presentId);

                    if (deletePresent != null)
                    {
                        _ChiniesSaleContext.Presents.Remove(deletePresent);
                        await _ChiniesSaleContext.SaveChangesAsync();
                    }

                    return await GetAllPresent();
                }
                else
                {
                    
                    return new List<present>();
                }
            }
        }


        public async Task<List<present>> UpdatePresent(int presentId, present presentUp)
        {
            if (presentUp != null)
            {
                present p = await _ChiniesSaleContext.Presents.FindAsync(presentId);

                if (p != null)
                {
                    p.Description = presentUp.Description;
                    p.Category = presentUp.Category;
                    p.Price = presentUp.Price;
                    p.Donors = presentUp.Donors;
                    p.Name = presentUp.Name;
                    p.ImagUrl = presentUp.ImagUrl;

                }

                await _ChiniesSaleContext.SaveChangesAsync();
                return await GetAllPresent();
            }
            return null;

        }
        public async Task<List<present>> GetPresentByName(string name)
        {
            if(name == "null" || name=="" || name=="undefined")
            {
             return await  _ChiniesSaleContext.Presents.ToListAsync();
            }
            else {
                var p = await _ChiniesSaleContext.Presents
                   .Where(p => p.Name.StartsWith(name)).ToListAsync();
                return p;
            }
           
        }

        public async Task<List<present>> GetPresentByCategoryDal(List<string> category)
        {
            if(!category.Any() || category.Count==0)
            {
                return  await _ChiniesSaleContext.Presents.ToListAsync();
            }
            else {
                List<present> p = new List<present>();
                foreach (var item in category)
                {
                    var presents = await _ChiniesSaleContext.Presents
                    .Where(p => p.Category == item).ToListAsync();
                    p.AddRange(presents);
                }

                return p;
            }
         
        }
        public async Task<List<present>> GetPresentByDonorDal(string donorName)
        {
            if(donorName==null) {
                return null;
            }
            else {
                var p = await _ChiniesSaleContext.Donations
                 .Where(d => d.Donor.DonorName == donorName)
                 .Select(p => p.Present)
                 .ToListAsync();
                return p;
            }
      
        }

        public async Task<List<present>> GetPresentByPriceDal(int price)
        {
         
            var p = await _ChiniesSaleContext.Presents
              .Where(p => p.Price == price)
              .ToListAsync();
            return p;
        }

        public async Task<List<present>> GetPresentByExpensiveDal()
        {
            var present = await _ChiniesSaleContext.Presents
                .OrderByDescending(p => p.Price)
                .ToListAsync() ;    
            return present;
        }
        public async Task<List<present>> GetPresentByCheapDal()
        {
            var present = await _ChiniesSaleContext.Presents
                .OrderBy(p => p.Price)
                .ToListAsync();
            return present;

        }

        public async Task<List<present>> GetByPurchasedDal()
        {
            var gifts = await _ChiniesSaleContext.Presents.ToListAsync();

            var orderedGifts = gifts.OrderByDescending(g => _ChiniesSaleContext.Orders.Count(o => o.presentId == g.PresentId)).ToList();

            return orderedGifts;

        }

    }
    }

