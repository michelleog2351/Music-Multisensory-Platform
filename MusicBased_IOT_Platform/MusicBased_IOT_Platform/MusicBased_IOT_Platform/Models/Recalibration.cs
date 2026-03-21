namespace MusicBased_IOT_Platform.Models
{
    public class Recalibration
    {
        public DateTime LastCalibrationDate { get; set; }
        public BaselineMetrics? Baseline { get; set; }
    }
}
