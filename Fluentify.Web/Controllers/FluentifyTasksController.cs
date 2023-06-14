using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Data;

namespace Fluentify.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FluentifyTasksController : ControllerBase
    {
        private readonly FluentifyDbContext _context;

        public FluentifyTasksController(FluentifyDbContext context)
        {
            _context = context;
        }

        // GET: api/FluentifyTasks
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var tasks = await _context.FluentifyTasks.ToListAsync();
                return Ok(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET: api/FluentifyTasks/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var task = await _context.FluentifyTasks.FindAsync(id);

                if (task == null)
                    return NotFound();

                return Ok(task);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST: api/FluentifyTasks
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FluentifyTask fluentifyTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.FluentifyTasks.Add(fluentifyTask);
                await _context.SaveChangesAsync();

                return CreatedAtAction("Get", new { id = fluentifyTask.Id }, fluentifyTask);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT: api/FluentifyTasks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FluentifyTask fluentifyTask)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != fluentifyTask.Id)
                return BadRequest();

            try
            {
                _context.Entry(fluentifyTask).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE: api/FluentifyTasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var task = await _context.FluentifyTasks.FindAsync(id);

                if (task == null)
                    return NotFound();

                _context.FluentifyTasks.Remove(task);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}