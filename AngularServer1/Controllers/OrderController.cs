using AngularServer1.BL;
using AngularServer1.Dto;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularServer1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
      
        private readonly IOrderService _order;
        public OrderController(IOrderService order)
        {
            this._order= order?? throw new ArgumentNullException(nameof(order));
        }
        [Authorize(Roles ="admin")]
        [HttpGet("TotalIncome")]
        public async Task<int> TotalIncome()
        {
            return await _order.TotalIncome();
        }
        [Authorize(Roles = "admin")]
        [HttpGet("getAllpurchases")]
        public async Task<List<object>> GetAllpurchases()
        {
            return await _order.GetAllpurchases();
        }

        [Authorize(Roles = "admin")]
        [HttpGet("getBuyersDetails")]
        public async Task<List<User>> GetBuyersDetails()
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            return await _order.GetBuyersDetails(middlewareUser.UserId);
        }
        [Authorize]
        [HttpGet("getBuyerPresent")]
        public async Task<List<present>> getBuyerPresent()
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            return await _order.getBuyerPresent(middlewareUser.UserId);
        }

        [Authorize]
        [HttpPost("addToCart")]
        public async Task<ActionResult<List<present>>> AddToCart([FromBody] int presentId)
        {

            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            var addToCart =  await _order.AddOrder(middlewareUser.UserId, presentId);
            if (addToCart!=null)
            {
                return Ok(addToCart);
            }
            return BadRequest("Error acuured");
        }

        [Authorize]
        [HttpPut("deleteFromCart")]
        public async Task<ActionResult<List<present>>> DeleteFromCart([FromBody] int presentId)
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            var deleteFromCart =  await _order.UpdateOrder(middlewareUser.UserId,presentId);
            if (deleteFromCart != null)
            {
                return Ok(deleteFromCart);
            }
             return  BadRequest("An error occurred while trying to delete present from cart");
        }
        [Authorize]
        [HttpPut("deleteFromCartOne")]
        public async Task<ActionResult<List<present>>> DeleteFromCartOne([FromBody] int presentId)
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            var deleteFromCart = await _order.UpdateOrderOne(middlewareUser.UserId, presentId);
            if (deleteFromCart != null)
            {
                return Ok(deleteFromCart);
            }
            return BadRequest("An error occurred while trying to delete present from cart");
        }

        [Authorize]
        [HttpPut("makePayment")]

        public async Task<ActionResult<List<Order>>> MakePayment([FromBody] List<int> order)
        {
            User middlewareUser = ControllerContext.HttpContext.Items["User"] as User;
            var payment =  await _order.PayForOrder(middlewareUser.UserId,order);
            if(payment!=null)
            {
                return Ok(payment);
            }
            return BadRequest("An error occurred while trying to pay for cart");
        }


    }
}
