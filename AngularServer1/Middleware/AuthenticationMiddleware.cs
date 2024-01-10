using AngularServer1.Modal;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using AngularServer1.BL;

namespace AngularServer1.Middleware
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuthenticationMiddleware> _logger;
  
        public AuthenticationMiddleware(RequestDelegate next, ILogger<AuthenticationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                var identity = context.User.Identity as ClaimsIdentity;
                if (identity == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var userClaims = identity.Claims;
                var user = new User
                {
                    UserId = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    //UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    //UserLastName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    UserPhone = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.HomePhone)?.Value,
                    UserCountry = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Country)?.Value,
                    UserEmail = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                    UserCity = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.StreetAddress)?.Value,
                    ////UserFirstName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.NameIdentifier)?.Value,
                    role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value,
                 
                
                };

                context.Items["User"] = user;
                await _next(context);

            }
               catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in the middleware.");
                
            }
        }
    
    }
}