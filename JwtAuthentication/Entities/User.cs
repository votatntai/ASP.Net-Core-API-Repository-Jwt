using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace JwtAuthentication.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public Guid Role { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}