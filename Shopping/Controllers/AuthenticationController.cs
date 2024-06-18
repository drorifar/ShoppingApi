using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Shopping.Models.Entities;
using Shopping.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shopping.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController(ILogger<AuthenticationController> _logger,
        IUserReposetory _repo,
        IConfiguration _config) : ControllerBase
    {
        public class AuthenticationRequest // we use it only here
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        [HttpPost]
        public async Task<ActionResult<string>> GetToken(AuthenticationRequest request) //the default of post is [frombody]
        {
            User? user = await _repo.AuthenticateUser(request.UserName, request.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            // generate jwt token

            List<Claim> claims = new List<Claim>() //the meta data list for the token
            {
                new Claim("sub", user.ID.ToString()),
                //new Claim("name", user.Name), //not recommend to add 
                //new Claim("email", user.Email), //not recommend to add 
                new Claim("auth_level", user.AuthLevel.ToString()),
                //new Claim("user_name", user.UserName), // DONT add to the claims because everyone can see it with the token
                //new Claim("password", user.Password)   // DONT add to the claims because everyone can see it with the token
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(
                Convert.FromBase64String(_config["Authentication:MyKey"]) 
                ); //generate key for the credentials (we getthe string from auto generate

            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //setting of the authentication method (key + algoritm)

            JwtSecurityToken token = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"], 
                claims,
                DateTime.UtcNow, //if we want that it will be a future toke
                DateTime.UtcNow.AddDays(1), //the expired time
                signingCredentials
                );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            string rawToken = handler.WriteToken(token);

            return Ok(rawToken);
        }

    }
}
