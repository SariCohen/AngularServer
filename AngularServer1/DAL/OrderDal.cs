using AngularServer1.Dto;
using AngularServer1.Modal;
using AngularServer1.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Any;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml.Linq;
namespace AngularServer1.DAL
{
    //לסדר את כל העניינים של הכמויות
    public class OrderDal : IOrderDal
    {
        private readonly IMapper mapper;
        private readonly ChiniesSaleContext _ChiniesSaleContext;

        public OrderDal(ChiniesSaleContext chiniesSaleContext, IMapper mapper)
        {
            this._ChiniesSaleContext = chiniesSaleContext ?? throw new ArgumentNullException(nameof(chiniesSaleContext));
            this.mapper = mapper;
        }


        public async Task<List<present>> AddOrderDal(string userId, int presentId)
        {
            var present = await _ChiniesSaleContext.Presents.FindAsync(presentId);
            if(present.isWinner==true) {
                return  null;
            }
            if (presentId != null && userId!=null) {
                
                List<present> userPresents = new List<present>();

                Order order = new Order();
                order.userId = userId;
                order.presentId = presentId;
                order.OrderDate = DateTime.Now.Date;
                order.status = "עגלה";
                await _ChiniesSaleContext.Orders.AddAsync(order);
                 await _ChiniesSaleContext.SaveChangesAsync();

                //var userOrders = await _ChiniesSaleContext.Orders
                //    .Where(o => o.userId == userId)
                //    .Include(o => o.Present)
                //    .Where(o => o.status == "עגלה")
                //    .ToListAsync();

                //userPresents.AddRange(userOrders.Select(ord => ord.Present));

                //return userPresents;
                return await getBuyerPresentDal(userId);
            }
            return null;
        }

        [Authorize]
        public async Task<List<present>> UpdateOrderDal(string userId, int presentId)
        {
            if (presentId != null && userId != null)
            {
                List<present> userPresents = new List<present>();

             
                var toUpdate = await _ChiniesSaleContext.Orders
                    .Where(i => i.presentId == presentId && i.userId == userId && i.status == "עגלה" )
                    .ToListAsync();

                if (toUpdate != null)
                {
                    foreach (var item in toUpdate)
                    {
                        item.OrderDate = DateTime.Now;
                        item.status = "נמחק";
                    }
                   

                    await _ChiniesSaleContext.SaveChangesAsync();

                    return await getBuyerPresentDal(userId);
                }
                else
                {

                    return null;
                }
            }
            return null;
        }


        public async Task<List<Order>> PayForOrderDal(string userId, List<int> order)
        {
            if(order != null && userId!=null)
            {
                foreach (var item in order)
                {
                    var toUpdate = await _ChiniesSaleContext.Orders.Where(i => i.presentId == item && i.userId == userId).ToListAsync();
                    foreach (var item1 in toUpdate)
                    {
                        item1.OrderDate = DateTime.Now;
                        item1.status = "שולם";
                    }
                }

                await _ChiniesSaleContext.SaveChangesAsync();
                return await _ChiniesSaleContext.Orders.ToListAsync();
            }
            return null;

        }
        public async Task<List<object>> GetAllpurchasesDal()
        {
            var purchases = await _ChiniesSaleContext.Presents
                .Select(present => new
                {
                    PresentName = present.Name,
                    PresentUrl = present.ImagUrl,
                    Purchases = _ChiniesSaleContext.Orders
                        .Where(order => order.presentId == present.PresentId && order.status == "שולם")
                        .Select(order => new
                        {
                            userImage= "../../../assets/images/פרופיל.png",
                            User = _ChiniesSaleContext.Users.FirstOrDefault(user => user.UserId == order.userId),
                            OrderDate = order.OrderDate,
                            
                        })
                        .ToList()
                })
                .ToListAsync();

            return purchases.Cast<object>().ToList();
        }


        public async Task<List<User>> GetBuyersDetailsDal()
            {
                var userIds = await _ChiniesSaleContext.Orders.Select(o => o.userId).ToListAsync();
                var distinctUserIds = userIds.Distinct();

                var buyersDetails = new List<User>();
                foreach (var userId in distinctUserIds)
                {
                var user = await _ChiniesSaleContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                buyersDetails.Add(user);
            }
                return buyersDetails;
            }
        public async Task<List<present>> getBuyerPresentDal(string userId)
        {
            List<present> userPresents = new List<present>();   
            var userOrders = await _ChiniesSaleContext.Orders.Where(o => o.userId==userId && o.status=="עגלה").ToListAsync();
            foreach (var order in userOrders)
            {
                present userPresent = await _ChiniesSaleContext.Presents.Where(p=>p.PresentId==order.presentId).FirstOrDefaultAsync();
                userPresents.Add(userPresent);
            }
            return   userPresents;
        }
        //public async Task<int> TotalIncomeDal()
        //{

        //   var allOrders = await _ChiniesSaleContext.Orders.Where(o=>o.status=="שולם").Select(o => o.presentId).ToListAsync();
        //    int sum = 0;    
        //    foreach (var presentOrd in allOrders)
        //    {
        //     var totalSum = _ChiniesSaleContext.Presents.Where(o=>o.PresentId==presentOrd).Select(p => p.Price).FirstOrDefault();
        //        sum += totalSum;
        //    }
        //    return sum;
        //}
        [Authorize(Roles ="admin")]
        public async Task<int> TotalIncomeDal()
        {
            var allOrders = await _ChiniesSaleContext.Orders
                .Where(o => o.status == "שולם")
                .Include(o => o.Present) 
                .Select(o => o)
                .ToListAsync();

            int sum = 0;

            foreach (var presentOrd in allOrders)
            {
                var totalSum = presentOrd.Present?.Price ?? 0;
                sum += totalSum;
            }

            return sum;
        }

        public async Task<List<present>> UpdateOrderOneDal(string userId, int presentId)
        {
            if (presentId != null && userId != null)
            {
                List<present> userPresents = new List<present>();


                var toUpdate = await _ChiniesSaleContext.Orders
                    .Where(i => i.presentId == presentId && i.userId == userId && i.status == "עגלה")
                    .FirstOrDefaultAsync();

                if (toUpdate != null)
                {
                  
                        toUpdate.OrderDate = DateTime.Now;
                        toUpdate.status = "נמחק";
                    


                    await _ChiniesSaleContext.SaveChangesAsync();

                    return await getBuyerPresentDal(userId);
                }
                else
                {

                    return null;
                }
            }
            return null;
        }
    } }

