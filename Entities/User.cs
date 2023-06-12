using System.Reflection;

namespace CarRentals.Entities
{
    public class User : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string HashSalt { get; set; }
        public string PasswordHash { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
        public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>(); 
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public ICollection<CarReport> CarReport { get; set; } = new HashSet<CarReport>();
    }
}
