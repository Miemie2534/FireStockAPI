namespace FireStockAPI.Models
{
    public class IncidentDto
    {
        public string Report { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Solution { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Recorder { get; set; } = string.Empty;

        public List<IFormFile>? Images { get; set; }
    }
}
