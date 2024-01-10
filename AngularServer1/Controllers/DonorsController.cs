using AngularServer1.BL;
using AngularServer1.Middleware;
using AngularServer1.Modal;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularServer1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DonorsController : ControllerBase
    {
        private readonly IDonorService _donor;
        private readonly ILogger<AuthenticationMiddleware> _logger;
        public DonorsController(IDonorService donor, ILogger<AuthenticationMiddleware> logger)
        {
            this._donor = donor ?? throw new ArgumentNullException(nameof(donor));
            _logger = logger;
        }

        //[Authorize]
        [HttpGet("getAllDonors")]
        public async Task<List<Donor>> GetAllDonors()
        {

            return await _donor.GetDonors();
           
        }

        [Authorize(Roles = "admin")]
        [HttpPost("addDonor")]
        public async Task<ActionResult<List<Donor>>> AddDonor([FromBody] Donor donor)
        {
            
            var donors =  await _donor.AddDonor(donor);
            if(donors != null)
            {
                return Ok(donors);
            }
            return BadRequest("Missing required fields");
            
        }

        [Authorize(Roles = "admin")]
        [HttpDelete("{doonrId}")]
        public async Task<ActionResult<List<Donor>>> DeleteDonor(string doonrId)
        {
            var donors =  await _donor.DeleteDonor(doonrId);
            if (donors != null)
            {
                return Ok(donors);
            }
            return BadRequest("An error occurred while trying to delete the donor");
        }

        [Authorize(Roles = "admin")]
        [HttpPut("{donorId}")]
        public async Task<ActionResult<List<Donor>>> UpdateDonor(string donorId, [FromBody] Donor donorUp)
        {
            var donors =  await _donor.UpdateDonor(donorId, donorUp);
            if (donors !=null)
            {
                return Ok(donors);   
            }
            return BadRequest("An error occurred while trying to update the donor");
        }



        [Authorize(Roles = "admin")]
        [HttpGet("/byEmail/{email}")]
        public async Task<List<Donor>> GetByEmail(string email)
        {
            return await _donor.GetDonorByEmail(email);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("/byPresent/{present}")]
        public async Task<List<Donor>> GetByPresent(string present)
        {
            return await _donor.GetDonorByPresent(present);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("/byName/{name}")]
        public async Task<List<Donor>> GetByName(string name)
        {
            return await _donor.GetDonorByName(name);
        }


    }
}

