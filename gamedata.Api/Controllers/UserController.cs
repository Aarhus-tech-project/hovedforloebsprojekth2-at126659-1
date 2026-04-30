using Microsoft.AspNetCore.Mvc;
using User.Models;
using User.Services;

namespace User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<AppUser>> GetAll() => UserService.GetAll();

        [HttpGet]
        [Route("{id}")]
        public ActionResult<AppUser> Get(int id)
        {
            var user = UserService.Get(id);
            if (user == null)
                return NotFound();
            return user;
        }

        [HttpGet]
        [Route("{id}/username")]
        public ActionResult<string> GetUsername(int id)
        {
            var user = UserService.Get(id);
            if (user == null)
                return NotFound();
            return user.Username;
        }

        [HttpPost]
        public ActionResult<AppUser> Post(string username, int? avatar, string password)
        {
            var createdUser = UserService.Post(username, avatar, password);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult<AppUser> Delete(int id)
        {
            var deletedUser = UserService.Delete(id);
            if (deletedUser == null)
                return NotFound();
            return NoContent();
        }
    }
}