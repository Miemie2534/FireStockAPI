namespace FireStockAPI.Models
{
    public class PatientDto
    {
        public DateTime ReportDate { get; set; }
        public int EmployeeId { get; set; }
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Sickness { get; set; } = string.Empty;
        public string Hospital { get; set; } = string.Empty;
        public string Recorder { get; set; } = string.Empty;
    }
}
