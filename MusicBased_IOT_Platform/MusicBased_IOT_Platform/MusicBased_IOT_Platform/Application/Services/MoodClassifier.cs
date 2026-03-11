using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    public static class MoodClassifier
    {
        public static MoodState ClassifyMood(BiometricSummary biometric)
        {
            if (biometric.AverageRestingHeartRate < 65 && biometric.AverageHeartRateVariability > 70)
                return MoodState.Calm;

            if (biometric.ActiveMinutes > 30)
                return MoodState.Active;

            if (biometric.AverageHeartRateVariability < 40)
                return MoodState.Restless;

            return MoodState.Neutral;
        }
    }
}
