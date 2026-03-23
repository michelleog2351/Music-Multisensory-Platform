namespace MusicBased_IOT_Platform.Models
{
    public class CalibrationRecord
    {
        public int ID { get; set; }

        public int UserID { get; set; }

        public double RestingHeartRate { get; set; }
        public double HRV { get; set; }
        public double BreathingRate { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
