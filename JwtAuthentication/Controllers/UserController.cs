using Microsoft.AspNetCore.Mvc;
using JwtAuthentication.Models;
using JwtAuthentication.Services;
using Microsoft.AspNetCore.Authorization;
using JwtAuthentication.Data;
using JwtAuthentication.Entities;
using System;

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
        public ActionResult<User> Register(UserRegisterModel model)
        {
            var user = new User
            {
                Name = model.Name,
                Username = model.Username,
                Email = model.Email,
                Role = Guid.Parse("9c076c5c-d4d9-4426-b6bf-da7b01c49d81"),
                Password = model.Password
                };
            _userService.Register(user);
            return CreatedAtAction("GetUser", new { Username = model.Username }, user);
        }

        [HttpGet("{Username}")]
        public User GetUser(string username)
        {
            var user = _userService.GetUser(username);
            return user;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}