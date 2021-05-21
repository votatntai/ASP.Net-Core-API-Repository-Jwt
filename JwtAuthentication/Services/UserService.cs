using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JwtAuthentication.Entities;
using JwtAuthentication.Helpers;
using JwtAuthentication.Models;
using JwtAuthentication.Data;
using Microsoft.EntityFrameworkCore;

namespace JwtAuthentication.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User Register(User user);
        User GetUser(string username);
        IEnumerable<User> GetAll();
        User GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly JwtAuthenticationContext _context;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, JwtAuthenticationContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.User.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

            if (user == null) return null;

            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public User Register(User user) {
            _context.User.Add(user);
            _context.SaveChanges();
            return user;
        }

        public User GetUser(string username)
        {
            var list = _context.User.ToList();
            var user = list.Find(o => o.Username.Equals(username));
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User.ToList();
        }

        public User GetById(Guid id)
        {
            return _context.User.FirstOrDefault(x => x.Id.Equals(id));
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}