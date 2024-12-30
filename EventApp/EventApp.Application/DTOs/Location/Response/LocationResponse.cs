namespace EventApp.Application.DTOs.Location.Response
{
    public record LocationResponse(
          string Address,
          string City,
          string State,
          string Country);
}
