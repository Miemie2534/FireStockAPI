using System.Text.Json.Serialization;

namespace FireStockAPI.Models
{
    public class FireExtinguisher
    {
        public int Id { get; set; }
        public string serialNumber { get; set; } = string.Empty;
        public string type { get; set; } = string.Empty;
        public string size { get; set; } = string.Empty;
        public string location { get; set; } = string.Empty;
        public string status { get; set; } = "พร้อมใช้งาน";
        public DateTime createdDate { get; set; } = DateTime.Now;

        public List<Claim> RepairClaims { get; set; } = new();
    }
}
