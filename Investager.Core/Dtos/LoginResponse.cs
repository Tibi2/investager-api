﻿using Investager.Core.Models;

namespace Investager.Core.Dtos;

public class LoginResponse
{
    public string DisplayName { get; set; }

    public Theme Theme { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}
