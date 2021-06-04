using Microsoft.AspNetCore.Mvc;
using JwtAuthentication.Services;
using System;
using JwtAuthentication.Data;
using JwtAuthentication.DataEntity;
using System.Linq;

namespace JwtAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("Register")]
        public ActionResult<UserResponse> Register(UserRegisterModel model)
        {
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Password = model.Password
            };
            var ur = new UserRole();
            ur.UserId =  userId ;
            ur.RoleId = Guid.Parse("72E1494C-CD14-4983-9BB0-0968C559C713");
            user.UserRoles.Add(ur);
            return _userService.Register(user);
        }

        [HttpGet("{id}")]
        public UserResponse GetUser(Guid id)
        {
            var u = _userService.GetById(id);
            return new UserResponse { 
            Id = u.Id,
            Role = u.UserRoles.Select(x => x.Role?.RoleName).ToArray(),
            Email = u.Email,
            Name = u.Name,
            Username = u.Username
            };
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] Pagination param)
        {
            var users = _userService.GetAll(param);
            return Ok(users);
        }
    }
}