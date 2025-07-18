using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FireStockAPI.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Report { get; set; } = string.Empty; // ชื่อผู้แจ้ง

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Location { get; set; } = string.Empty; // สถานที่เกิดเหตุ

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Description { get; set; } = string.Empty; // รายละเอียดการเกิดเหตุ

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Solution { get; set; } = string.Empty; // การแก้ไข

        [FromForm(Name ="Type")]
        public string Type { get; set; } = "เหตุการณ์";

        [Required(ErrorMessage = "กรุณากรอกข้อมูล")]
        public string Recorder { get; set; } = string.Empty; // ผู้บันทึกข้อมูลฃ
    }
}
