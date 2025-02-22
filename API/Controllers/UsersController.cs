using System.Security.Claims;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(UserRepository _repository, PhotoService _photoService, IMapper _mapper) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetUsers()
        {
            var users = await _repository.GetAllUsersAsync();
            var usersToReturn = _mapper.Map<IEnumerable<UserResponseDto>>(users);
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

            return _mapper.Map<UserResponseDto>(result);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<UserResponseDto>> GetUserByUsername(string username)
        {
            User.FindFirst(ClaimTypes.NameIdentifier);

            var result = await _repository.GetUserByUsernameAsync(username);
            if (result is null)
            {
                return NotFound(string.Empty);
            }

            return _mapper.Map<UserResponseDto>(result);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateUser(UpdateUserDto updateUser)
        {
            if (string.IsNullOrEmpty(updateUser.KnownAs))
            {
                return BadRequest("Required field knownAs cannot be null");
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("No username attached to the request");
            }

            var user = await _repository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return BadRequest($"Cannot find username by id '{username}'");
            }

            _mapper.Map(updateUser, user);

            if (await _repository.SaveAllAsync())
            {
                return NoContent();
            }

            return BadRequest("Cannot apply the user update changes");
        }

        [HttpPost("image")]
        public async Task<ActionResult<PhotoDto>> AddUserPhoto(IFormFile uploadedFile)
        {
            if (uploadedFile.Length == 0)
            {
                return BadRequest("Bad file upload");
            }

            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("No username attached to the request");
            }

            var user = await _repository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return BadRequest($"Cannot find username by id '{username}'");
            }

            var result = await _photoService.AddPhotoAsync(uploadedFile);
            if (result == null)
            {
                return BadRequest("Failed to add photo");
            }

            var photo = new Photo()
            {
                FileName = result.FileName,
                IsMain = false
            };

            user.Photos.Add(photo);
            if (await _repository.SaveAllAsync())
            {
                return _mapper.Map<PhotoDto>(photo);
            }

            return BadRequest("Could not save transactional changes");
        }
    }
}
