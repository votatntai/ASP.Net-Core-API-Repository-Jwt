using JwtAuthentication.Entities;
using System;

namespace JwtAuthentication.Models
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid Role { get; set; }
        public string Token { get; set; }


        public AuthenticateResponse(User user, string token)
        {
            Id = user.Id;
            Name = user.Name;
            Username = user.Username;
            Email = user.Email;
            Role = user.Role;
            Token = token;
        }
    }
}