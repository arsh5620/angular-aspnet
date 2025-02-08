using System.Security.Cryptography;
using System.Text;
using API;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController(UserRepository _repository, AuthTokenService _tokenService) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<ActionResult<LoggedInUserDto>> RegisterUser(RegisterUserDto registerUser)
        {
            if (await CheckIfUserExists(registerUser.Username))
            {
                return BadRequest("User already exists");
            }

            return Ok();
            // using var hmac = new HMACSHA512();

            // var user = new AppUser()
            // {
            //     UserName = registerUser.Username,
            //     PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUser.Password)),
            //     PasswordSalt = hmac.Key
            // };

            // await _context.Users.AddAsync(user);
            // await _context.SaveChangesAsync();

            // return new LoggedInUserDto()
            // {
            //     Username = registerUser.Username,
            //     AuthToken = _tokenService.GenerateToken(user)
            // };
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