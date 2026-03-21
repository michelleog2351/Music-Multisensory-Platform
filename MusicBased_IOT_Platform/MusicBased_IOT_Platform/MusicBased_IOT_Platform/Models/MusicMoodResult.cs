namespace MusicBased_IOT_Platform.Models
{
    public class MusicMoodResult
    {
        public BiometricSummary? BiometricSummary { get; set; }

        public MoodState Mood { get; set; }

        public List<Track>? RecommendedTracks { get; set; }

        public DateTime GeneratedAt { get; set; }
    }
}