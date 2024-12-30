namespace EventApp.Domain.ValueObjects
{
    public class Location
    {
        public string Address { get; set; } = null!; 
        public string City { get; set; } = null!;
        public string State { get; set; } = null!; 
        public string Country { get; set; } = null!; 

        public override string ToString()
        {
            return $"{Address}, {City}, {State}, {Country}";
        }
    }
}
