﻿namespace Investager.Core.Dtos
{
    public class LoginResponse
    {
        public string FirstName { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }
    }
}