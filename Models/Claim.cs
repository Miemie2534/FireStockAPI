using System.Text.Json.Serialization;

namespace FireStockAPI.Models
{
    public class Claim
    {
        public int Id { get; set; }
        public int FireExtinguisherId { get; set; }
        public string Claims { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;

        public DateTime claimDate { get; set; } = DateTime.Now;

        [JsonIgnore]
        public FireExtinguisher? FireExtinguisher { get; set; } 
    }
}
