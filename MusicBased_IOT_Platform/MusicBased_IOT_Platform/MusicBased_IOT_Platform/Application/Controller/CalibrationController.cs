using Microsoft.AspNetCore.Mvc;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Services;

namespace MusicBased_IOT_Platform.Application.Controller
{
        [ApiController]
        [Route("api/[controller]")]
        public class CalibrationController : ControllerBase
        {
            private readonly IUserContext _userContext;
            private readonly ICalibrationRepository _calibrationRepo;

            public CalibrationController(
                IUserContext userContext,
                ICalibrationRepository calibrationRepo)
            {
                _userContext = userContext;
                _calibrationRepo = calibrationRepo;
            }

            [HttpGet("history")]
            public async Task<IActionResult> GetHistory([FromQuery] int days = 7)
            {
                var user = await _userContext.GetCurrentUserAsync();

                if (user == null)
                    return Unauthorized();

            Console.WriteLine($"USER: {user!.FirstName}");

            //var history = await _calibrationRepo.GetByUserIDAsync(user.ID);
            var history = await _calibrationRepo.GetRecentAsync(user.ID, days);

                return Ok(history);
            }
        }
}
