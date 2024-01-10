using AngularServer1.BL;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularServer1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinnerController : ControllerBase
    {
        private readonly IWinnerService _winner;
        public WinnerController(IWinnerService winner)
        {
            this._winner = winner ?? throw new ArgumentNullException(nameof(winner));

        }

        [Authorize(Roles = "admin")]
        [HttpPost("MakingLottery")]
        public async Task<ActionResult<User>> MakeLottery([FromBody] int presentId)
        {

            var makeLottery =  await _winner.AddWinner(presentId);
            if (makeLottery != null)
            {
                return Ok(makeLottery);
            }
            else
            {
                return BadRequest("cant make lottery twice");    
            }
        }
        [Authorize]
        [HttpPost("GetWinnerPresent")]
        public async Task<ActionResult<User>> GetPresentWinner([FromBody] int presentId)
        {
            var userWinner= await _winner.getPresentWinner(presentId);
            if(userWinner == null)
            {
                return NotFound("No winner found");
            }
            else
            {
                return Ok(userWinner);
            }
                 
            
        }

    }
}
