using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AngularServer1.DAL
{
    public class WinnerDal : IWinnerDal
    {
        private readonly ChiniesSaleContext _ChiniesSaleContext;

        public WinnerDal(ChiniesSaleContext chiniesSaleContext)
        {
            this._ChiniesSaleContext = chiniesSaleContext ?? throw new ArgumentNullException(nameof(chiniesSaleContext));
        }
  
        public async Task<User> AddWinnerDal(int presentId)
        {
        // רוצה להגריל יצירת רשימה של כל המתנות שהוזמנו שהם המתנה שאני 
        var presentOrders = _ChiniesSaleContext.Orders
                .Include(o => o.Present)
        .Where(o => o.presentId == presentId && o.status == "שולם")
        .ToList();

            if (presentOrders[0].Present.isWinner == false)
            {

                Random random = new Random();
                int randomIndex = random.Next(presentOrders.Count);
                Order winnerOrd = presentOrders[randomIndex];
                Winner winnerToAdd = new Winner();
                // לאחר שהגרלנו ,ליצור זוכה חדש
                winnerToAdd.Order = winnerOrd;
                await _ChiniesSaleContext.Winners.AddAsync(winnerToAdd);
                await _ChiniesSaleContext.SaveChangesAsync();


                //להחזיר את הזוכה
                var winner = _ChiniesSaleContext.Winners
                    .Where(w => w.Order.presentId == presentId && w.Order.status == "שולם")
                    .Include(w => w.Order)
                    .ThenInclude(o => o.user)

                    .FirstOrDefault();
                foreach (var item in presentOrders)
                {
                    if (item.Present != null)
                    {
                        item.Present.isWinner = true;
                        await _ChiniesSaleContext.SaveChangesAsync();
                    }
                }
               
                    return winner.Order.user;
                
            }
            return null;

        }

        public async Task<User> getPresentWinnerDal(int presentId)
        {
            var winner = await _ChiniesSaleContext.Winners
                .Include(o=>o.Order)
               .ThenInclude(u=>u.user)
            
                .Where(w => w.Order.presentId == presentId && w.Order.status == "שולם" )
                .Select(w => new
                {
                    User = w.Order.user,
                  
                })
                .FirstOrDefaultAsync();

            if (winner != null)
            {
      
                return winner.User;
            }

            return null;
        }
    }

        //public async Task<User> getPresentWinnerDal(int presentId)
        //{

        //        // קבל את ההזמנה (Order) המקושרת למתנה הנתונה
        //        var order = await _ChiniesSaleContext.Orders
        //            .Include(o => o.user) // כדי לקבל גם את פרטי המשתמש שהזמין
        //            .Where(o => o.presentId == presentId && o.status == "שולם")
        //            .FirstOrDefaultAsync();

        //        if (order != null)
        //        {
        //            // קבל את הזוכה (Winner) שמקושר להזמנה זו
        //            var winner = await _ChiniesSaleContext.Winners
        //                .Include(w => w.Order)
        //                .ThenInclude(o => o.user) // גם כאן, כדי לקבל גם את פרטי המשתמש שהזמין
        //                .Where(w => w.Order.OrderId == order.OrderId)
        //                .FirstOrDefaultAsync();

        //            if (winner != null)
        //            {
        //                return winner.Order.user; // החזר את פרטי המשתמש שהזמין וזכה במתנה
        //            }
        //        }

        //        return null; // אם לא נמצא זוכה, החזר null
        //    }
        //public async Task<User> getPresentWinnerDal(int presentId)
        //{
        //    //var winner = await _ChiniesSaleContext.Winners
        //    //    .Where(w => w.Order.presentId == presentId && w.Order.status == "שולם")
        //    //    .Include(w => w.Order)
        //    //    .ThenInclude(o => o.user)
        //    //    .ThenInclude(u => u.present) // Include גם את טבלת המתנות
        //    //    .FirstOrDefaultAsync();

        //    //if (winner != null)
        //    //{
        //    //    return winner.order.user;
        //    //}

        //    return null;
        //}

    
    }

