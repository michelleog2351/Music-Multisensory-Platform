namespace MusicBased_IOT_Platform.Models
{
    public class FitbitProfileResponse
    {
        public FitbitProfile? User { get; set; }
    }

    public class ActivityResponse
    {
        public ActivitySummary? ActivitiesSummary { get; set; }
    }

    public class HeartRateResponse
    {
        public List<HeartRateSummary>? ActivitiesHeart { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SleepResponse
    {
        public SleepSummary? Summary { get; set; }
    }

    public class StepsResponse
    {
        public List<Steps>? ActivitiesSteps { get; set; }
    }
}