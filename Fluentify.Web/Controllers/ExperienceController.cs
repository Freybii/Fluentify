using Fluentify.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fluentify.Web.Controllers
{
    [ApiController]
    [Route("api/exp")]
    public class ExperienceController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ExperienceController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateExperience()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return Unauthorized();

            user.Experience = (user.Experience ?? 0) + 100;
            await _userManager.UpdateAsync(user);

            return Ok();
        }
        [HttpGet]
        public async Task<IActionResult> GetUserExperience([FromQuery] string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return NotFound();

            return Ok(user.Experience);
        }
    }

}