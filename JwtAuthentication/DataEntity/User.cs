using System;
using System.Collections.Generic;

namespace JwtAuthentication.DataEntity
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
