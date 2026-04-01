using Microsoft.AspNetCore.Mvc;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Services;

namespace MusicBased_IOT_Platform.Controller
{
    [ApiController]
    [Route("api/calibration")]
    public class CalibrationController(
        IUserContext userContext,
        ICalibrationRepository calibrationRepo) : ControllerBase
    {
        private readonly IUserContext _userContext = userContext;
        private readonly ICalibrationRepository _calibrationRepo = calibrationRepo;

        [HttpGet("history")]
        public async Task<IActionResult> GetHistory([FromQuery] int days = 7)
        {
            //var user = await _userContext.GetCurrentUserAsync();

            //if (user == null)
            //    return Unauthorized();


            var user = await _userContext.GetCurrentUserAsync();

            if (user == null)
                Console.WriteLine("⚠️ No user found - using fallback userId = 1");
            user = new Models.UserAccount { ID = 1 };

            Console.WriteLine($"USER: {user!.FirstName}");

            //var history = await _calibrationRepo.GetByUserIDAsync(user.ID);
            var history = await _calibrationRepo.GetRecentAsync(user.ID, days);

            //return Ok(history);
            return Ok(new[]
            {
                new { restingHeartRate = 70, hrv = 50, breathingRate = 12, createdAt = DateTime.Now },
                new { restingHeartRate = 75, hrv = 48, breathingRate = 13, createdAt = DateTime.Now},
                });
             }
        }
}
