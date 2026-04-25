namespace MusicBased_IOT_Platform.Models
{
    public class CalibrationRecord
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public double RestingHeartRate { get; set; }
        public double HRV { get; set; }
        public double BreathingRate { get; set; }

        public string Mood { get; set; } = string.Empty;
        public string TracksJson { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
