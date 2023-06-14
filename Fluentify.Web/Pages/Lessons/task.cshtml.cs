using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Controllers;
using Fluentify.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Fluentify.Web.Pages.Lessons
{
    public class taskModel : PageModel
    {
        private readonly FluentifyDbContext _context;

        public taskModel(FluentifyDbContext context)
        {
            _context = context;
        }

        // Add a property to store the task retrieved from the database
        public FluentifyTask Task { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            try
            {
                // Retrieve the task from the database based on the provided id
                Task = await _context.FluentifyTasks.FindAsync(id);

                if (Task == null)
                    return NotFound();

                return Page();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}