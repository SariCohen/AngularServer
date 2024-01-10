using AngularServer1.DAL;
using AngularServer1.Modal;
using AngularServer1.Models;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AngularServer1.BL
{
    public class UserService:IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IConfiguration _config;
        public UserService(IUserDal userDal, IConfiguration config)
        {
            this._userDal = userDal;
            this._config = config;

        }

        public async Task<List<User>> AddUser(User user)
        {
            return await _userDal.AddUserDal(user);
        }

        //public User ValidateToken(string token)
        //{
        //    // Split the token to get the username and expiry date
        //    string[] tokenParts = token.Split(':');

        //    if (tokenParts.Length == 2)
        //    {
        //        string username = tokenParts[0];
        //        string expiryDateString = tokenParts[1];

        //        // Validate the expiry date
        //        if (DateTime.TryParse(expiryDateString, out DateTime expiryDate))
        //        {
        //            // Check if the token is still valid
        //            if (expiryDate > DateTime.Now)
        //            {
        //                // Create and return a new User object

        //                User user = new User();
        //                user.UserName = username;
        //                return user;
        //            }
        //        }
        //    }

        //    // In case the token is invalid or expired, return null or throw an exception
        //    return null;
        //}

        public string Generate(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                 new Claim(ClaimTypes.NameIdentifier,user.UserId),
                //new Claim(ClaimTypes.NameIdentifier,user.UserFirstName),
                //new Claim(ClaimTypes.NameIdentifier,user.UserLastName),
                //new Claim(ClaimTypes.NameIdentifier,user.UserName),
                new Claim(ClaimTypes.HomePhone,user.UserPhone),
                new Claim(ClaimTypes.Country,user.UserCountry),
                new Claim(ClaimTypes.Email,user.UserEmail),
                new Claim(ClaimTypes.StreetAddress,user.UserCity),
                new Claim(ClaimTypes.Role,user.role),
               
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<User> Authenticate(UserLogin userLogin)
        {
            return await _userDal.AuthenticateDal(userLogin);
        }
    }

}

