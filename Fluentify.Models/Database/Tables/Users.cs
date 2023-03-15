using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Fluentify.Models.Database.Tables
{
    public class Users : DbItem
    {
        public string? Nickname { get; set; }
        public string? Email { get; set;}
        public decimal? PhoneNumber { get; set; }
        public string? Password { get; set; }
        public string? RegDate { get; set; }

    }
}
