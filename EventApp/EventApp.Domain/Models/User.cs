namespace EventApp.Domain.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public Dictionary<Guid, DateTime> EventRegistrationDates { get; set; } = new();
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }

        public List<Event> Events { get; set; } = new();
       
    }

}
