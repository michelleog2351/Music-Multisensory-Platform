using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    /// <summary>
    /// The MoodClassifier class is responsible for classifying the user's mood based on their biometric data.
    /// </summary>
    public static class MoodClassifier
    {
        //public static MoodState ClassifyMood(BiometricSummary biometric)
        //{
        //    if (biometric.AverageRestingHeartRate < 65 && biometric.AverageHeartRateVariability > 70)
        //        return MoodState.Calm;

        //    if (biometric.AverageActiveMinutes > 30)
        //        return MoodState.Active;

        //    if (biometric.AverageHeartRateVariability < 40)
        //        return MoodState.Restless;

        //    return MoodState.Neutral;
        //}
        public static MoodState ClassifyMood(
    BiometricSummary current,
    CalibrationRecord? baseline)
        {
            if (baseline == null)
                return MoodState.Neutral;

            var hrDiff = current.AverageRestingHeartRate - baseline.RestingHeartRate;
            var hrvDiff = current.AverageHeartRateVariability - baseline.HRV;

            if (hrDiff > 10 && hrvDiff < -10)
                return MoodState.Stressed;

            if (hrDiff < -5 && hrvDiff > 5)
                return MoodState.Calm;

            if (current.AverageActiveMinutes > baseline.RestingHeartRate)
                return MoodState.Active;

            return MoodState.Neutral;
        }
    }
}
