
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FireStockAPI.Models
{
    public class CreateRepairClaimDto
    {
        public int FireExtinguisherId { get; set; }
        public string Claims { get; set; } = string.Empty;
        public string ActionTaken { get; set; } = string.Empty;
    }
}
