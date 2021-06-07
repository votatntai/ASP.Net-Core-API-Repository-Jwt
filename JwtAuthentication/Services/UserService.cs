using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using JwtAuthentication.Helpers;
using JwtAuthentication.Data;
using JwtAuthentication.DataEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace JwtAuthentication.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        UserResponse Register(User user);
        IEnumerable<UserResponse> GetAll(Pagination param);
        User GetById(Guid id);
    }

    public class UserService : IUserService
    {
        private readonly AuthenticationShopContext _context;

        private readonly AppSettings _appSettings;

        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IOptions<AppSettings> appSettings, AuthenticationShopContext context, IHttpContextAccessor httpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _context.Users.Where(x => x.Username == model.Username && x.Password == model.Password).Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefault();

            if (user == null) return null;

            var token = generateJwtToken(user);

            var u = new AuthenticateResponse
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                Role = user.UserRoles.Select(x => x.Role?.RoleName).ToArray(),
                Username = user.Username,
                Token = token
            };
            return u;
        }

        public UserResponse Register(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            var u = GetById(user.Id);
            var ur = new UserResponse
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                Name = u.Name,
                Role = u.UserRoles.Select(x => x.Role?.RoleName).ToArray()
            };
            return ur;
        }

        public IEnumerable<UserResponse> GetAll(Pagination param)
        {
            return _context.Users.Include(x => x.UserRoles).ThenInclude(x => x.Role).Select(x => new UserResponse
            {
                Id = x.Id,
                Username = x.Username,
                Email = x.Email,
                Name = x.Name,
                Role = x.UserRoles.Select(x => x.Role.RoleName).ToArray()
            }).OrderBy(x => x.Email).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToList();
        }

        public User GetById(Guid id)
        {
            return _context.Users.Where(x => x.Id.Equals(id)).Include(x => x.UserRoles).ThenInclude(x => x.Role).FirstOrDefault();
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var id = user.Id.ToString();
            var roles = String.Join(",", user.UserRoles.Select(x => x.Role.RoleName));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()), new Claim("roles", String.Join(",", user.UserRoles.Select(x => x.Role.RoleName))) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var t = tokenHandler.WriteToken(token);
            return tokenHandler.WriteToken(token);
        }
    }
}