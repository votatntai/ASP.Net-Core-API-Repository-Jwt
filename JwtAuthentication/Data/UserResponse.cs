using System;
using System.Collections.Generic;

namespace JwtAuthentication.Data
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
