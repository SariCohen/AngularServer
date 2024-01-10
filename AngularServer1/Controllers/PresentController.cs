using AngularServer1.BL;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularServer1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresentController : ControllerBase
    {
        private readonly IPresentService _present;
        public PresentController(IPresentService present)
        {
            this._present = present?? throw new ArgumentNullException(nameof(present));

        }

        [HttpGet("getAllPresents")]

        public async Task<ActionResult<List<present>>> GetPresents()
        {
            var presents = await _present.GetPresents();

            if (presents != null && presents.Count > 0)
            {
                return Ok(presents);
            }

            return NotFound("No presents found");
        }

        
        [Authorize(Roles = "admin")]
        [HttpPost("addPresent")]
        public async Task<ActionResult<List<present>>> AddPresent([FromBody] present present)
        {
           
            var added= await _present.AddPresent(present);
            if (added != null){
                return Ok(added);
            }
            return BadRequest("missing requires fiels");
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{presentId}")]
        public async Task<ActionResult<List<present>>> DeletePresent(int presentId)
        {
            var deleted =  await _present.DeletePresent(presentId);
            if(deleted !=null && deleted.Count > 0)
            {
                return Ok(deleted);
            }
            return BadRequest(null);
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{presentId}")]
        public async Task<ActionResult<List<present>>> UpdatePresent(int presentId, [FromBody] present presentUp)
        {
           var updated =  await _present.UpdatePresent(presentId, presentUp);
            if (updated == null)
            {
                return BadRequest("Missing fields are required"); 
            }
            return Ok( updated);
        }



        [HttpGet("byName/{name}")]
        public async Task  <ActionResult<List<present>>> GetByName(string name)
        {
            var getByName = await _present.GetPresentByName(name);
            if (getByName.Count==0 || getByName==null)
            {
                return NotFound("Threre is no presents with that name");
            }
            return Ok( getByName);   
        }

        [HttpGet("byDonor/{donorName}")]
        public async Task< ActionResult< List<present>>> GetByDonor(string donorName)
        {
            var getByDonor = await _present.GetPresentByDonor(donorName);
            if (getByDonor==null) {
                return BadRequest("Nissing filels are required");
            }
            return Ok( getByDonor);
        }
        [HttpPost("byCategory")]
        public async Task<ActionResult<List<present>>> GetByCategory([FromBody] List<string> category)
        {
            var getByCategory = await _present.GetPresentByCategory(category);
            if (getByCategory == null)
            {
                return BadRequest("Nissing filels are required");
            }
            return Ok(  getByCategory);
        }
        [HttpGet("byPrice/{price}")]
        public async Task<ActionResult<List<present>>> GetByPrice(int price)
        {
            var getByPrice =  await _present.GetPresentByPrice(price);
            if (getByPrice == null)
            {
                return BadRequest("Nissing filels are required");
            }
            return Ok(getByPrice);
        }

        [HttpGet("byMostExpensive")]
        public async Task<ActionResult<List<present>>> GetByExpensivePrice()
        {
            var byExpensive= await _present.GetByExpensivePrice();
            if (byExpensive==null)
            {
                return NotFound("No presents found");
            }
            return Ok(byExpensive);
        }
        [HttpGet("byMostCheap")]
        public async Task<ActionResult<List<present>>> GetByCheapPrice()
        {
            var byCheap = await _present.GetByCheapPrice();
            if(byCheap==null)
            {
                return NotFound("No presents found");
            }
            return Ok(byCheap);
        }

        [HttpGet("byMostPurchased")]
        public async Task<ActionResult<List<present>>> GetByPurchased()
        {
            var mostPurchased = await _present.GetByPurchased();
            if(mostPurchased==null)
            {
                return NotFound("No presents found");
            }
            return Ok(mostPurchased);
        }
    }
}
