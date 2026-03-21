using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public class MappingService
    {
        public BiometricSummary MapToBiometricSummary(
            HeartRateSummary heartRate,
            ActivitySummary activity)
        {
            var summary = new BiometricSummary()
            {
                AverageRestingHeartRate = heartRate.RestingHeartRate,
                AverageDailySteps = activity.Steps,
                AverageActiveMinutes = activity.ActiveMinutes,
                AverageBreathingRate = CalculateBreathingRate(heartRate),
                AverageHeartRateVariability = CalculateHRV(heartRate),
                IsCalmState = heartRate.RestingHeartRate < 70 && activity.ActiveMinutes < 20,
                CapturedAt = DateTime.UtcNow
            };

            return summary;
        }

        /// <summary>
        /// The CalculateHRV
        /// </summary>
        /// <param name="heartRate"></param>
        /// <returns></returns>
        private static double CalculateHRV(HeartRateSummary heartRate)
        {
            if (heartRate.Zones == null || heartRate.Zones.Count == 0)
                return 0;

            return heartRate.Zones.Average(z => (z.Min + z.Max) / 2.0);
        }

        /// <summary>
        /// The 
        /// </summary>
        /// <param name="heartRate"></param>
        /// <returns></returns>
        private static double CalculateBreathingRate(HeartRateSummary heartRate)
        {
            return heartRate.RestingHeartRate / 4.0;
        }

        public static MoodState ClassifyMood(BiometricSummary bio)
        {
            if (bio.AverageRestingHeartRate < 65 && bio.AverageHeartRateVariability > 70)
                return MoodState.Calm;

            if (bio.AverageActiveMinutes > 30)
                return MoodState.Active;

            if (bio.AverageHeartRateVariability < 40)
                return MoodState.Restless;

            return MoodState.Neutral;
        }
    }
}

