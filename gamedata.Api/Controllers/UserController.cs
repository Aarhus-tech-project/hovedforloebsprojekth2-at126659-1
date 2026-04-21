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
    }
}