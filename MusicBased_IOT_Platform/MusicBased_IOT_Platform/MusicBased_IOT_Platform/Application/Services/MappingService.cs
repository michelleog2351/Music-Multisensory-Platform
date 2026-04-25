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
        /// This is a very simplified calculation for heart rate variability (HRV) based on the average of the heart rate zones. In a real application, you would use more sophisticated methods to calculate HRV, such as analyzing the time intervals between heartbeats (RR intervals) using data from a heart rate monitor that provides this information.
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
    }
}

