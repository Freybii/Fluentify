using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CognitiveServices.Speech;
using Microsoft.Extensions.Configuration;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace Fluentify.Web.Controllers
{
    [Route("api/speech")]
    [ApiController]
    public class SpeechController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SpeechController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("synthesize")]
        public async Task<IActionResult> Synthesize([FromBody] SynthesizeRequest request)
        {
            var config = SpeechConfig.FromSubscription(_configuration["AzureCognitiveServices:SubscriptionKey"], _configuration["AzureCognitiveServices:Region"]);
            using var synthesizer = new SpeechSynthesizer(config);
            using var result = await synthesizer.SpeakTextAsync(request.Text);

            if (result.Reason == ResultReason.SynthesizingAudioCompleted)
            {
                // Get the synthesized audio as a byte array
                var audioData = result.AudioData;

                // Return the audio as a base64-encoded string
                var audioUrl = $"data:audio/wav;base64,{Convert.ToBase64String(audioData)}";

                return Ok(new SynthesizeResponse { AudioUrl = audioUrl });
            }
            else if (result.Reason == ResultReason.Canceled)
            {
                var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
                // Handle cancellation

                return BadRequest();
            }
            return BadRequest();
        }
    }

    public class SynthesizeRequest
    {
        public string Text { get; set; }
    }

    public class SynthesizeResponse
    {
        public string AudioUrl { get; set; }
    }
}
