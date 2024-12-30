namespace EventApp.Application.DTOs.Location.Request
{
    public record LocationRequest(
        string? Address,
        string? City,
        string? State,
        string? Country);
}
