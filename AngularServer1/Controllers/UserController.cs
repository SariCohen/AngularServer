using AngularServer1.BL;
using AngularServer1.DAL;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AngularServer1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ChiniesSaleContext _ChiniesSaleContext;

        private readonly IConfiguration _config;

        private readonly IUserService _user;
   
        public UserController(IConfiguration config, ChiniesSaleContext chiniesSaleContext, IUserService user)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _ChiniesSaleContext = chiniesSaleContext ?? throw new ArgumentNullException(nameof(chiniesSaleContext));
            _user = user ?? throw new ArgumentNullException(nameof(user));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<List<User>>> Register([FromBody] User user)
        {
            if (user.PostalCode==null || user.UserPassword == null || user.UserEmail == null || user.UserAddress == null ||
                
                user.UserCountry==null || user.UserId==null || user.UserPhone==null || user.UserName==null ||   user.UserLastName==null

                || user.UserFirstName==null || user.role==null || user.UserCity==null) 
            {

                return NotFound("Details are missing");

            }
            else
            {
                var registered = await _user.AddUser(user);
                if(registered != null) {
                    return Ok(registered);
                }
                return BadRequest("An error ucurred while trying to registered");
               
            }
         
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] UserLogin userLogin)
        {
            var user = await _user.Authenticate(userLogin);

            if (user != null)
                {
                object token = _user.Generate(user);
                var jsonToken = JsonConvert.SerializeObject(new { token });
                return Ok(new { jsonToken });
                //return Ok(token);
               }
            return NotFound("User not found");
        }
    }
}
