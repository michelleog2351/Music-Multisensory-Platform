using System.Text.Json.Serialization;

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
        [JsonPropertyName("activities-heart")]
        public List<ActivityHeart>? ActivitiesHeart { get; set; }
    }

    public class ActivityHeart
    {
        [JsonPropertyName("value")]
        public HeartRateSummary? Value { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class SleepResponse
    {
        public SleepSummary? Summary { get; set; }
    }

    //[JsonPropertyName("activities-steps")]
    public class StepsResponse
    {
        public List<Steps>? ActivitiesSteps { get; set; }
    }
}