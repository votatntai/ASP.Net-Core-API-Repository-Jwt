using JwtAuthentication.DataEntity;
using System;

namespace JwtAuthentication.Data
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string[] Role { get; set; }
        public string Token { get; set; }
    }
}