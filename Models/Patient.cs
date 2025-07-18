namespace FireStockAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public DateTime ReportDate { get; set; } = DateTime.Now;
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public string Sickness { get; set; } = string.Empty;
        public string Hospital { get; set; } = string.Empty;
        public string Recorder { get; set; } = string.Empty;
    }
}
