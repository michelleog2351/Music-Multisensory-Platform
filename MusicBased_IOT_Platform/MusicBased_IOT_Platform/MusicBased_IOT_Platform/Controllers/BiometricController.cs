using Microsoft.AspNetCore.Mvc;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Controller
{
    [ApiController]
    [Route("api/biometric")]
    public class BiometricController : ControllerBase
    {
        [HttpPost("data")]
        public IActionResult ReceiveBiometricData([FromBody] BiometricSummary data)
        {
            return Ok(new { message = "Biometric data received successfully", receivedData = data });

            // save to database or process the data as needed
            /// Low HR and high HRV could indicate a calm state, while high HR and low HRV might indicate stress & anxiety
            /// HR between 60-80 bpm and HRV above 50 ms could be a good indicator of a calm state.
            /// Low HR + no movement + low breathing = sleeping state
            /// High HR + low HRV + high breathing = stressed state
            /// High HR + high HRV + moderate breathing = active state
            /// cardio - energetic musicc high tempo, energy - workout music

            // call the Spotify Search API to get music recommendations based on the biometric data
        }
    }
}
