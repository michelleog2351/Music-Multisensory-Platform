using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Services
{
    /// <summary>
    /// The constructor for the MoodCalibrationService initializes the necessary services for
    /// </summary>
    /// <param name="fitbitDataService"></param>
    /// <param name="spotifyDataService"></param>
    public class MoodCalibrationService(IFitbitDataService fitbitDataService, ISpotifyDataService spotifyDataService)
    {
        private readonly IFitbitDataService _fitbitDataService = fitbitDataService;
        private readonly ISpotifyDataService _spotifyDataService = spotifyDataService;

        /// <summary>
        /// The MoodCalibrationService is responsible for determining if a recalibration 
        /// of the user's mood is necessary based on their biometric data and providing 
        /// music recommendations accordingly.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsRecalibrationRequiredAsync()
        {
            var lastCalibration = await GetLastCalibrationDate();

            return (DateTime.Now - lastCalibration).TotalDays > 14;
        }

        /// <summary>
        /// The RecalibrateAsync method retrieves the latest biometric data from the Fitbit service,
        /// </summary>
        /// <returns></returns>
        public async Task<MusicMoodResult> RecalibrateAsync()
        {
            var biometric = await _fitbitDataService.GetBiometricDataAsync();
            var mood = MoodClassifier.ClassifyMood(biometric);

            var (danceability, energy, valence, liveness) = MapMoodToSpotify(mood);

            var tracks = await _spotifyDataService.GetMoodRecommendations(
                10,
                danceability,
                energy,
                valence,
                liveness);

            return new MusicMoodResult
            {
                BiometricSummary = biometric,
                Mood = mood,
                RecommendedTracks = [.. tracks.Tracks!]
            };

        }

        public async Task<MusicMoodResult> GenerateMoodMusicAsync()
        {
            var biometric = await _fitbitDataService.GetBiometricDataAsync();

            var mood = MoodClassifier.ClassifyMood(biometric);

            var (danceability, energy, valence, liveness) = MapMoodToSpotify(mood);

            var tracks = await _spotifyDataService.GetMoodRecommendations(
                10,
                danceability,
                energy,
                valence,
                liveness);


            return new MusicMoodResult
            {
                BiometricSummary = biometric,
                Mood = mood,
                RecommendedTracks = [.. tracks.Tracks!]
            };
        }

        private async Task<DateTime> GetLastCalibrationDate()
        {
            // TEMP placeholder until database integration
            await Task.CompletedTask;

            return DateTime.Now.AddDays(-20);
        }

        private static (double danceability, double energy, double valence, double liveness) MapMoodToSpotify(MoodState mood)
        {
            return mood switch
            {
                MoodState.Calm => (0.4, 0.3, 0.6, 0.2),
                MoodState.Active => (0.8, 0.8, 0.7, 0.5),
                MoodState.Restless => (0.3, 0.4, 0.3, 0.3),
                _ => (0.5, 0.5, 0.5, 0.5)
            };
        }

        private async Task SaveCalibrationAsync(BiometricSummary biometric)
        {
            await Task.CompletedTask;

            // later this will store baseline biometrics
        }
    }
}
