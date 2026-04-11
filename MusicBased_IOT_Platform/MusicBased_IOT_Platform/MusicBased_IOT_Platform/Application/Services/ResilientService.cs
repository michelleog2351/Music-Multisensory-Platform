using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public class ResilientFitbitService(
        IFitbitDataService live,
        IFitbitDataService fallback) : IFitbitDataService
    {
        private readonly IFitbitDataService _live = live;
        private readonly IFitbitDataService _fallback = fallback;

        public Task<bool> AuthCodeFlowAsync(string code, int userID)
        {
            throw new NotImplementedException();
        }

        public async Task<BiometricSummary> GetBiometricDataAsync()
        {
            try
            {
                return await _live.GetBiometricDataAsync();
            }
            catch
            {
                return await _fallback.GetBiometricDataAsync();
            }
        }

        public Task<ActivitySummary> GetDailyActivityAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<HeartRateSummary> GetDailyHeartRateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<Steps> GetDistanceInStepsAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public string GetFitbitAuthUrl()
        {
            throw new NotImplementedException();
        }

        public Task<List<HeartRateZone>> GetHeartRateZonesAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<FitbitProfile> GetProfileAsync()
        {
            throw new NotImplementedException();
        }

        public Task<SleepSummary> GetSleepAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasValidTokenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<MusicMoodResult> ReadBiometricAndMusicDataAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> TestDataConnectionAsync()
        {
            throw new NotImplementedException();
        }
    }
}
