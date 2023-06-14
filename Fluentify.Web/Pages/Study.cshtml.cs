using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Fluentify.Web.Pages
{
    public class StudyModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly FluentifyDbContext _context;

        public List<FluentifyTask> Tasks { get; set; }

        public StudyModel(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, FluentifyDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        public async Task OnGetAsync()
        {
            if (_signInManager.IsSignedIn(User))
            {
                Tasks = await _context.FluentifyTasks.ToListAsync();
            }
        }
    }
}
