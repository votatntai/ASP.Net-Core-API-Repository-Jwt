using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using JwtAuthentication.Entities;

namespace JwtAuthentication.Data
{
    public class JwtAuthenticationContext : DbContext
    {
        public JwtAuthenticationContext (DbContextOptions<JwtAuthenticationContext> options)
            : base(options)
        {
        }

        public DbSet<JwtAuthentication.Entities.User> User { get; set; }
    }
}
