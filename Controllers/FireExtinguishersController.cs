using FireStockAPI.Data;
using FireStockAPI.Models;
using FireStockAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FireExtinguishersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly JwtService _jwtService;

        public FireExtinguishersController(ApplicationDbContext context, JwtService jwt)
        {
            _context = context;
            _jwtService = jwt;
        }

        // GET: api/FireExtinguishers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FireExtinguisher>>> GetAll()
        {
            var fireExtinguishers = await _context.fireExtinguishers
            .Include(f => f.RepairClaims)   // โหลดข้อมูล RepairClaims ด้วย
            .ToListAsync();

            return Ok(fireExtinguishers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FireExtinguisher>> GetFireExtinguisherById(int id)
        {
            var extinguisher = await _context.fireExtinguishers
                .Include(f => f.RepairClaims)
                .FirstOrDefaultAsync(f => f.Id == id);

            if (extinguisher == null)
            {
                return NotFound("ไม่พบข้อมูลสต๊อกที่ต้องการค้นหา");
            }

            return Ok(extinguisher);
        }


        [HttpPost]
        public async Task<ActionResult<FireExtinguisher>> CreateFire(FireExtinguisher model)
        {
            try
            {
                await _context.fireExtinguishers.AddAsync(model);
                await _context.SaveChangesAsync();
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest("ไม่สามารเพิ่มข้อมูลสต๊อกได้");
            }
        }
        


        [HttpDelete("{id}")]
        public async Task<ActionResult<FireExtinguisher>> DeleteFireExtinguisher(int id)
        {
            var extinguisher = await _context.fireExtinguishers.FindAsync(id);
            if (extinguisher == null)
            {
                return NotFound("ไม่พบข้อมูลสต๊อกที่ต้องการลบ");
            }
            _context.fireExtinguishers.Remove(extinguisher);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
