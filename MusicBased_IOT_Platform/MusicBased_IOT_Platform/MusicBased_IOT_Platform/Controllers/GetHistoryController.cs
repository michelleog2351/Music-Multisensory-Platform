using Microsoft.AspNetCore.Mvc;
using MusicBased_IOT_Platform.Application.Services;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Controller
{
    //[HttpGet("history")]
    //public async Task<IActionResult> GetHistory(int days = 7)
    //{
    //    //var user = await _userContext.GetCurrentUserAsync();
    //    //if (user == null) return Unauthorized();

    //    //var data = await _calibrationRepo.GetRecentAsync(user.ID, days);

    //    //var result = data.Select(x => new
    //    //{
    //    //    date = x.CreatedAt.ToString("yyyy-MM-dd"),
    //    //    mood = x.Mood,
    //    //    heartRate = x.RestingHeartRate,

    //    //    // Convert JSON back to object
    //    //    tracks = string.IsNullOrEmpty(x.TracksJson)
    //    //        ? new List<object>()
    //    //        : JsonSerializer.Deserialize<List<object>>(x.TracksJson)
    //    //});

    //    //return Ok(result);
    //}
}
