using FireStockAPI.Data;
using FireStockAPI.Models;
using FireStockAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(ApplicationDbContext context, JwtService jwtService, IPasswordHasher<User> passwordHasher)
        {
            _jwtService = jwtService;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] Register register )
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.username == register.username);
                if (existingUser != null)
                {
                    return BadRequest("กรุณากรอกให้ถูกต้อง");
                }

                var user = new User
                {
                    username = register.username,
                    Role = register.role,
                };

                user.passwordHash = _passwordHasher.HashPassword(user, register.password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return Ok(new { message = "สมัครสมาชิกสำเร็จ", user.username });
            }
            catch (Exception ex) 
            {
                return StatusCode(500, "เกิดข้อผิดพลาด: " + ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] LoginDto login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.username == login.Username);
            if (user == null) return Unauthorized("ไม่พบผู้ใช้ กรุณาลองใหม่อีกครั้ง");

            var result = _passwordHasher.VerifyHashedPassword(user, user.passwordHash, login.Password);
            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("รหัสผ่านไม่ถูกต้อง");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token, user.username, user.Role });
        }

        [HttpGet("admin-only")]
        [Authorize(Roles ="admin")]
        public IActionResult OnlyCanSee()
        {
            return Ok("ข้อมูลแสดงสำหรับแอดมิน");
        }
    }
}
