namespace MusicBased_IOT_Platform.Models
{
    public class HeartRateSummary
    {
        public double RestingHeartRate { get; set; }

        public List<HeartRateZone>? Zones { get; set; }
    }
}