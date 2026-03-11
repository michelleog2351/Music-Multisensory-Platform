namespace MusicBased_IOT_Platform.Models
{
    public class ActivitySummary
    {
        public int Steps { get; set; }
        public int ActiveMinutes { get; set; }
        public double CaloriesBurned { get; set; }

        public List<Distance>? Distances { get; set; }
    }
}