﻿namespace ELCLite.Identity.Models.Identity
{
    public record UserAuthenicatedModel
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
