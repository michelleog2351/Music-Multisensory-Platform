namespace MusicBased_IOT_Platform.Models
{
    public class BiometricSummary
    {
        public double AverageRestingHeartRate { get; set; }
        public double AverageHeartRateVariability { get; set; }
        public double AverageBreathingRate { get; set; }
        public double AverageDailySteps { get; set; }
        public double AverageActiveMinutes { get; set; }

        public DateTime CapturedAt { get; set; }

        public bool IsCalmState { get; set; }
    }
}