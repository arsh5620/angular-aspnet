using System.Security.Cryptography;
using System.Text;
using API;
using API.Data;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(UserRepository _repository, AuthTokenService _tokenService, IMapper _mapper) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<LoggedInUserDto>> RegisterUser(RegisterUserDto registerUser)
        {
            if (await CheckIfUserExists(registerUser.Username))
            {
                return BadRequest("User already exists");
            }

            using var hmac = new HMACSHA512();

            var user = new AppUser()
            {
                UserName = registerUser.Username,
                KnownAs = registerUser.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)),
                PasswordSalt = hmac.Key,
                Gender = "male",
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow),
                Created = DateTime.Now,
                LastActive = DateTime.UtcNow,
                City = "some city",
                Country = "some country"
            };

            await _repository.AddUserAsync(user);
            return new LoggedInUserDto()
            {
                Username = user.UserName,
                AuthToken = _tokenService.GenerateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoggedInUserDto>> Login(LoginRequestDto loginRequest)
        {

            var user = await _repository.GetUserByUsernameAsync(loginRequest.Username);
            if (user is null)
            {
                return Unauthorized("Username incorrect");
            }

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var hashedKey = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginRequest.Password));
            var authenticated = hashedKey.SequenceEqual(user.PasswordHash);
            if (!authenticated)
            {
                return Unauthorized("Password incorrect");
            }

            return new LoggedInUserDto()
            {
                Username = loginRequest.Username,
                AuthToken = _tokenService.GenerateToken(user)
            };
        }

        async Task<bool> CheckIfUserExists(string userName)
        {
            return await _repository.CheckIfUsernameExistsAsync(userName);
        }
    }
}