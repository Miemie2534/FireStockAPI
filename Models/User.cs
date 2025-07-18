namespace FireStockAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public string passwordHash { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
    }
}
