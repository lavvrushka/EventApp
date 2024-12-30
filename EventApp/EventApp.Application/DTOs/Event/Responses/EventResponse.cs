using EventApp.Application.DTOs.Location.Response;
using EventApp.Application.DTOs.User.Responses;

namespace EventApp.Application.DTOs.Event.Responses
{
    public class EventResponse 
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public LocationResponse Location { get; set; }
        public string Category { get; set; }
        public int MaxUsers { get; set; }
        public string ImageData { get; set; }
        public string ImageType { get; set; }

        public Guid ImageId { get; set; }
        public List<UserResponse>? Users { get; set; }

        public EventResponse() { }
        public EventResponse(
            Guid id,
            string title,
            string description,
            DateTime date,
            LocationResponse location,
            string category,
            int maxUsers,
            string imageData,
            string imageType,
             Guid imageId,
            List<UserResponse>? users = null)
        {
            Id = id;
            Title = title;
            Description = description;
            DateTime = date;
            Location = location;
            Category = category;
            MaxUsers = maxUsers;
            ImageData = imageData;
            ImageType = imageType;
            ImageId = imageId;
            Users = users ?? new List<UserResponse>();
        }
    }
}
