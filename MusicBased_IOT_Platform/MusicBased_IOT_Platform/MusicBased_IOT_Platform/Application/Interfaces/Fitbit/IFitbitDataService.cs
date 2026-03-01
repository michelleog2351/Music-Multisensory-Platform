using Microsoft.AspNetCore.Connections.Features;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces.Fitbit
{
    public interface IFitbitDataService
    {
        /// <summary>
        /// The <c>AuthCodeFlowAsync</c> method takes an authorization code as a parameter 
        /// and uses it to acquire an access token from the Fitbit API. 
        /// It returns a boolean indicating whether the authentication was successful or not.
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        Task<bool> AuthCodeFlowAsync(string code);

        /// <summary>
        /// The <c>HasValidTokenAsync</c> method checks if the current access token is valid 
        /// and has not expired. It returns a boolean indicating whether the token is valid or not.
        /// </summary>
        /// <returns></returns>
        Task<bool> HasValidTokenAsync();

        /// <summary>
        /// The <c>GetProfileAsync</c> method retrieves the user's profile information from 
        /// the Fitbit API.
        /// </summary>
        /// <returns></returns>
        Task<FitbitProfile> GetProfileAsync();

        /// <summary>
        /// The <c>GetDailyHeartRateAsync</c> method retrieves the user's heart rate data 
        /// for a specific date from the Fitbit API and returns a summary of the heart rate information.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<HeartRateSummary> GetDailyHeartRateAsync(DateTime date);

        /// <summary>
        /// The <c>GetHeartRateZonesAsync</c> method retrieves the user's heart rate zone data for a specific date from the Fitbit API and returns a list of heart rate zones, each containing information about the time spent in that zone and the corresponding heart rate range.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<List<HeartRateZone>> GetHeartRateZonesAsync(DateTime date);

        /// <summary>
        /// The <c>GetDistanceInStepsAsync</c> method retrieves the user's distance data for a specific date from the Fitbit API and returns the distance in steps.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<Distance> GetDistanceInStepsAsync(DateTime date);

        /// <summary>
        /// The <c>GetSleepAsync</c> method retrieves the user's sleep data for a specific date from the
        /// Fitbit API and returns a summary of the sleep information.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<SleepSummary> GetSleepAsync(DateTime date);

        /// <summary>
        /// The <c>GetDailyActivityAsync</c> method retrieves the user's activity data for a specific
        /// date from the Fitbit API and returns a summary of the activity information, including steps taken, distance traveled, calories burned, and active minutes.
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        Task<ActivitySummary> GetDailyActivityAsync(DateTime date);

        /// <summary>
        /// The
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        Task ReadBiometricAndMusicDataAsync()
        {
          
        }

        /// <summary>
        /// The <c>TestDataConnection</c> method tests if the client has a valid data connection
        /// to the data service. 
        /// </summary>
        /// <returns><c>True</c> if it has a valid data connection, otherwise <c>false</c>.</returns>
        Task<bool> TestDataConnectionAsync();
    }
}
