using FireStockAPI.Data;
using FireStockAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FireStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthController(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register model)
        {
            var user = new ApplicationUser
            {
                UserName = model.username,
                Email = model.email,
                FullName = model.FullName,
            };

            var result = await _userManager.CreateAsync(user, model.password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            if (!await _roleManager.RoleExistsAsync(model.role))
            {
                await _roleManager.CreateAsync(new IdentityRole(model.role));
            }

            await _userManager.AddToRoleAsync(user, model.role);
            return Ok(new { message = "User registered successfully" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.Username); // Await the Task to get the actual ApplicationUser object
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRole = await _userManager.GetRolesAsync(user);
                var authClaims = new List<System.Security.Claims.Claim>
               {
                   new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name, model.Username),
                   new System.Security.Claims.Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
               };

                authClaims.AddRange(userRole.Select(role => new System.Security.Claims.Claim(ClaimTypes.Role, role)));

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],

                    audience: _configuration["Jwt:Audience"],
                    claims: authClaims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
                        SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo
                });
            }
            return Unauthorized(new { message = "Invalid username or password" });
        }
    }
}
