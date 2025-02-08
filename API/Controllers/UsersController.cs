using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(UserRepository _repository, IMapper mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            var usersToReturn = mapper.Map<IEnumerable<UserResponseDto>>(users);
            return Ok(usersToReturn);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponseDto>> GetUserById(int id)
        {
            var result = await _repository.GetUserByIdAsync(id);
            if (result is null)
            {
                return NotFound(string.Empty);
            }

            return mapper.Map<UserResponseDto>(result);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserResponseDto>> GetUserByUsername(string username)
        {
            var result = await _repository.GetUserByUsernameAsync(username);
            if (result is null)
            {
                return NotFound(string.Empty);
            }

            return mapper.Map<UserResponseDto>(result);
        }
    }
}
