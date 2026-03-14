namespace MusicBased_IOT_Platform.Models
{
    public class FitbitProfileResponse
    {
        public FitbitProfile? User { get; set; }
    }

    public class ActivityResponse
    {
        public ActivitySummary? Summary { get; set; }
    }

    public class HeartRateResponse
    {
        public List<HeartRateSummary>? ActivitiesHeart { get; set; }
    }

    public class SleepResponse
    {
        public SleepSummary? Summary { get; set; }
    }

    public class StepsResponse
    {
        public List<Distance>? ActivitiesSteps { get; set; }
    }
}