using System.Text.Json.Serialization;

namespace FireStockAPI.Models
{
    public class UpdateRequest
    {
        [JsonPropertyName("model")]
        public FireExtinguisher Model { get; set; }

        [JsonPropertyName("repairClaims")]
        public List<CreateRepairClaimDto> RepairClaims { get; set; }
    }
}
