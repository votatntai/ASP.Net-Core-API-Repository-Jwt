using System.Collections.Generic;

namespace JwtAuthentication.Data
{
    public class UserRegisterWithRole
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<string> Roles { get; set; }
    }
}
