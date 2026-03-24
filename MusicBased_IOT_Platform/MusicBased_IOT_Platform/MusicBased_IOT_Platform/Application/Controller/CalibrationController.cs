using Microsoft.AspNetCore.Mvc;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Services;

namespace MusicBased_IOT_Platform.Application.Controller
{
        [ApiController]
        [Route("api/calibration")]
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
            public async Task<IActionResult> GetHistory()
            {
                var user = await _userContext.GetCurrentUserAsync();

                if (user == null)
                    return Unauthorized();

                var history = await _calibrationRepo.GetByUserIDAsync(user.ID);

                return Ok(history);
            }
        }
}
