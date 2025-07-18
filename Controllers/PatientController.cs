using FireStockAPI.Data;
using FireStockAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FireStockAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;


    public PatientController(ApplicationDbContext context)
        {
            _context = context;
        }



        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAllPatient()
        {
            var data = await _context.Patients.ToListAsync();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Patient>> GetPatientById(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound("ไม่พบข้อมูลผู้ป่วยที่ต้องการค้นหา");
            }

            return Ok(patient);
        }

        [HttpPost]
        public async Task<ActionResult<PatientDto>> AddPatient([FromBody] PatientDto request)
        {
            try
            {
                var patient = new Patient
                {
                    ReportDate = request.ReportDate,
                    EmployeeId = request.EmployeeId,
                    firstName = request.firstName,
                    lastName = request.lastName,
                    Department = request.Department,
                    Sickness = request.Sickness,
                    Hospital = request.Hospital,
                    Recorder = request.Recorder
                };

                await _context.Patients.AddAsync(patient);
                await _context.SaveChangesAsync();
                return Ok(request);
            }
            catch
            {
                return BadRequest("ไม่สามารถเพิ่มข้อมูลผู้ป่วยได้");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient (int id, Patient request)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound("ไม่พบข้อมูลผู้ป่วยที่ต้องการแก้ไข");
            }

            patient.ReportDate = request.ReportDate;
            patient.EmployeeId = request.EmployeeId;
            patient.firstName = request.firstName;
            patient.lastName = request.lastName;
            patient.Department = request.Department;
            patient.Sickness = request.Sickness;
            patient.Hospital = request.Hospital;
            patient.Recorder = request.Recorder;

            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();

            return Ok(new { message = "อัพเดทข้อมูลเรียบร้อย"});
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Patient>> DeletePatient(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient != null)
            {
                await _context.Patients.Where(x => x.Id == id).ExecuteDeleteAsync();
                await _context.SaveChangesAsync();
                return Ok(patient);
            }
            return NotFound("ไม่พบข้อมูลผู้ป่วยที่ต้องการลบ");
        }
    }
}
