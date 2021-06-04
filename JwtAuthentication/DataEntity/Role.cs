using System;
using System.Collections.Generic;

namespace JwtAuthentication.DataEntity
{
    public partial class Role
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }
        public string RoleName { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
