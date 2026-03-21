namespace MusicBased_IOT_Platform.Models
{
    public class BaselineMetrics
    {
        public double RestingHeartRate { get; set; }

        public double HeartRateVariability { get; set; }

        public double BreathingRate { get; set; }

        public DateTime CalibrationDate { get; set; }
    }
}