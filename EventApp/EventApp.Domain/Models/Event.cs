using EventApp.Domain.ValueObjects;

namespace EventApp.Domain.Models
{
    public class Event
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime DateTime { get; set; }

        public Location Location { get; set; } = null!;

        public string Category { get; set; }

        public int MaxUsers { get; set; }

        public List<User>? Users { get; set; } = new();

        public Guid ImageId { get; set; }

        public Image? Image { get; set; }
    }
}
