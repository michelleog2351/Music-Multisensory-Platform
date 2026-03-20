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
    //public class MoodCalibrationService( IFitbitDataService live, IFitbitDataService fallback)
   public class MoodCalibrationService(IFitbitDataService fitbitDataService, ISpotifyDataService spotifyDataService)
    {
        private readonly IFitbitDataService _fitbitDataService = fitbitDataService;
        //private readonly IFitbitDataService _live = live;
        //private readonly IFitbitDataService _fallback = fallback;

       private readonly ISpotifyDataService _spotifyDataService = spotifyDataService;

        //public async Task<BiometricSummary> GetBiometricDataAsync()
        //{
        //    try
        //    {
        //        return await _live.GetBiometricDataAsync();
        //    }
        //    catch
        //    {
        //        return await _fallback.GetBiometricDataAsync();
        //    }
        //}


        /// <summary>
        /// The MoodCalibrationService is responsible for determining if a recalibration 
        /// of the user's mood is necessary based on their biometric data and providing 
        /// music recommendations accordingly.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> IsRecalibrationRequiredAsync()
        {
            var lastCalibration = await GetLastCalibrationDate();

            return (DateTime.UtcNow - lastCalibration).TotalDays > 14;
        }

        /// <summary>
        /// The RecalibrateAsync method retrieves the latest biometric data from the Fitbit service,
        /// </summary>
        /// <returns></returns>
        public async Task<MusicMoodResult> RecalibrateAsync()
        {
            //try catch?????

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
                RecommendedTracks = tracks?.Tracks?.ToList() ?? []
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

            //if (tracks == null)
            //{
                return new MusicMoodResult
                {
                    BiometricSummary = biometric,
                    Mood = mood,
                    RecommendedTracks = tracks!.Tracks?.ToList() ?? []
                };
           // }
        }

        private static async Task<DateTime> GetLastCalibrationDate()
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

        private static async Task SaveCalibrationAsync(BiometricSummary biometric)
        {
            await Task.CompletedTask;

            // later this will store baseline biometrics
        }
    }
}
