using Fluentify.Web.Areas.Identity.Data;
using Fluentify.Web.Controllers;
using Fluentify.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.CognitiveServices.Speech;

namespace Fluentify.Web.Pages.Lessons
{
    public class taskModel : PageModel
    {
        private readonly FluentifyDbContext _context;
        private readonly IConfiguration _configuration;

        public taskModel(FluentifyDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
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

                // Perform text-to-speech conversion
                await SynthesizeTextToSpeech(Task.Text);

                return Page();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        private async Task SynthesizeTextToSpeech(string text)
        {
            var config = SpeechConfig.FromSubscription(_configuration["AzureCognitiveServices:SubscriptionKey"], _configuration["AzureCognitiveServices:Region"]);
            using var synthesizer = new SpeechSynthesizer(config);
            using var result = await synthesizer.SpeakTextAsync(text);

            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                // Save the synthesized audio to a file or stream
                // For example: result.AudioData can be saved to a WAV file
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                // Handle cancellation
            }
        }
    }
}