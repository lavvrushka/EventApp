﻿namespace EventApp.Application.DTOs.User.Responses
{

    public record UserLoginResponse(
      string AccessToken,
      string RefreshToken
      
  );
}

