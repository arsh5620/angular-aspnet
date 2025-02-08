using API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController(DefaultDbContext context) : ControllerBase
    {
        [HttpGet("server-error")]
        public ActionResult<bool> GetServerError()
        {
            throw new NullReferenceException();
            return false;
        }

        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("Why the fuck are you bothering me!");
        }
    }
}
