using FireStockAPI.Data;
using FireStockAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FireStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncidentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Incident>>> GetAllIncident()
        {
            var data = await _context.Incidents
                .ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Incident>> GetIncidentById(int id)
        {
            var data = await _context.Incidents
                .FirstOrDefaultAsync(p => p.Id == id);

            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

       

        [HttpPut("{id}")]
        [RequestSizeLimit(50_000_000)]
        public async Task<ActionResult> UpdateIncident(int id,
            [FromForm] string Report,
            [FromForm] string Location,
            [FromForm] string Type,
            [FromForm] string Description,
            [FromForm] string Solution,
            [FromForm] string Recorder,
            [FromForm] DateTime ReportDate,
            [FromForm] List<IFormFile>? Images // รองรับการแนบรูปใหม่ (ถ้ามี)
)
        {
            try
            {
                var incident = await _context.Incidents
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (incident == null)
                {
                    return NotFound("ไม่พบข้อมูลเหตุการณ์ที่ต้องการแก้ไข");
                }

                // อัปเดตข้อมูลทั่วไป
                incident.Report = Report;
                incident.Location = Location;
                incident.Type = Type;
                incident.Description = Description;
                incident.Solution = Solution;
                incident.Recorder = Recorder;
                incident.ReportDate = ReportDate;

                // หากแนบรูปภาพมาใหม่ ให้ลบรูปเดิมแล้วเพิ่มใหม่
                //if (Images != null && Images.Count > 0)
                //{
                //    // ลบรูปเดิม
                //    _context.IncidentImages.RemoveRange(incident.Images);
                //    incident.Images = new List<IncidentImage>();

                //    foreach (var file in Images)
                //    {
                //        using var ms = new MemoryStream();
                //        await file.CopyToAsync(ms);
                //        incident.Images.Add(new IncidentImage
                //        {
                //            FileName = file.FileName,
                //            ImageData = ms.ToArray()
                //        });
                //    }
                //}

                await _context.SaveChangesAsync();
                return Ok(new { message = "อัปเดตข้อมูลสำเร็จ" });
            }
            catch (Exception ex)
            {
                return BadRequest($"เกิดข้อผิดพลาดในการอัปเดต: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Incident>> DeleteIncident(int id)
        {
            var data = await _context.Incidents.FirstOrDefaultAsync(p => p.Id == id);
            if (data == null)
            {
                return NotFound();
            }

            _context.Incidents.Remove(data);
            await _context.SaveChangesAsync();

            return Ok(data);
        }
    }
}
