namespace EventApp.Application.DTOs.User.Responses
{
    public record UserTokenRespones(
        string AccessToken,
        string RefreshToken
    );
}
