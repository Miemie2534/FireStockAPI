using FireStockAPI.Data;
using FireStockAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepairClaimsController : ControllerBase
    {
        private readonly ApplicationDbContext _claim;

        public RepairClaimsController(ApplicationDbContext claim)
        {
            _claim = claim;
        }

        // GET: api/RepairClaims/with-extinguisher
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var claims = await _claim.claims
         .Include(c => c.FireExtinguisher)
         .Select(c => new
         {
             c.Id,
             c.FireExtinguisherId,
             c.Claims,
             c.claimDate,
             c.ActionTaken,
             SerialNumber = c.FireExtinguisher != null ? c.FireExtinguisher.serialNumber : string.Empty,
             Type = c.FireExtinguisher != null ? c.FireExtinguisher.type : string.Empty,
             Size = c.FireExtinguisher != null ? c.FireExtinguisher.size : string.Empty,
         })
         .ToListAsync();
            return Ok(claims);
        }

        //GET: api/RepairClaims/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CreateRepairClaimDto>> GetById(int id)
        {
            var claim = await _claim.claims
                .Include(rc => rc.FireExtinguisher)
                .FirstOrDefaultAsync(rc => rc.Id == id);

            if (claim == null)
            {
                return NotFound();
            }

            return Ok(claim);
        }

        // POST: api/RepairClaims
        [HttpPost]
        public async Task<IActionResult> CreateClaim([FromBody] CreateRepairClaimDto dto)
        {
            var extinguisher = await _claim.fireExtinguishers.FindAsync(dto.FireExtinguisherId);
            if (extinguisher == null)
            {
                return NotFound("ไม่พบถังดับเพลิง");
            }
            var claim = new Claim
            {
                FireExtinguisherId = dto.FireExtinguisherId,
                
                Claims = dto.Claims,
                ActionTaken = dto.ActionTaken
            };
            _claim.claims.Add(claim);
            await _claim.SaveChangesAsync();

            return Ok(new { message = "บันทึกการเคลมเรียบร้อย"});
        }



        // PUT: api/RepairClaims/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, CreateRepairClaimDto model)
        {
            var claim = await _claim.claims.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }
            claim.Claims = model.Claims;
            claim.ActionTaken = model.ActionTaken;
            await _claim.SaveChangesAsync();
            return Ok();
        }

        //Delete
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClaims(int id)
        {
            var existingClaim = await _claim.claims.FindAsync(id);
            if (existingClaim == null)
            {
                return NotFound("ไม่พบการเคลมที่ต้องการลบ");
            }
            _claim.claims.Remove(existingClaim);
            await _claim.SaveChangesAsync();

            return Ok(new { message = "ลบการเคลมเรียบร้อย" });
        }
    }
}
