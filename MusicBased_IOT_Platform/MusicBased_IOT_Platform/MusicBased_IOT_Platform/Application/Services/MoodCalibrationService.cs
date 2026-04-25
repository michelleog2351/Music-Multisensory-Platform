//using MusicBased_IOT_Platform.Application.Interfaces;
//using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
//using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
//using MusicBased_IOT_Platform.Models;
//using System.Text.Json;

//namespace MusicBased_IOT_Platform.Application.Services
//{
//    /// <summary>
//    /// The constructor for the MoodCalibrationService initializes the necessary services for
//    /// </summary>
//    /// <param name="fitbitDataService"></param>
//    /// <param name="spotifyDataService"></param>
//    //public class MoodCalibrationService( IFitbitDataService live, IFitbitDataService fallback)
//    public class MoodCalibrationService(IFitbitDataService fitbitDataService, ISpotifyDataService spotifyDataService, IUserContext userContext, ICalibrationRepository calibrationRepository)
//    {
//        private readonly IFitbitDataService _fitbitDataService = fitbitDataService;
//        private readonly ISpotifyDataService _spotifyDataService = spotifyDataService;

//        private readonly IUserContext _userContext = userContext;
//        private readonly ICalibrationRepository _calibrationRepo = calibrationRepository;

//        /// <summary>
//        /// The MoodCalibrationService is responsible for determining if a recalibration 
//        /// of the user's mood is necessary based on their biometric data and providing 
//        /// music recommendations accordingly.
//        /// </summary>
//        /// <returns></returns>
//        public static async Task<bool> IsRecalibrationRequiredAsync()
//        {
//            var lastCalibration = await GetLastCalibrationDate();

//            return (DateTime.UtcNow - lastCalibration).TotalDays > 14;
//        }

//        /// <summary>
//        /// The RecalibrateAsync method retrieves the latest biometric data from the Fitbit service,
//        /// </summary>
//        /// <returns></returns>
//        public async Task<MusicMoodResult> BuildMoodResultAsync()
//        {
//            //try catch?????

//            var biometric = await _fitbitDataService.GetBiometricDataAsync();
//            var mood = MoodClassifier.ClassifyMood(biometric);

//            var (danceability, energy, valence, liveness) = MapMoodToSpotify(mood);

//            var tracks = await _spotifyDataService.GetMoodRecommendations(
//                10,
//                danceability,
//                energy,
//                valence,
//                liveness);

//            return new MusicMoodResult
//            {
//                BiometricSummary = biometric,
//                Mood = mood,
//                RecommendedTracks = tracks?.Tracks?.ToList() ?? [],
//                GeneratedAt = DateTime.UtcNow
//            };

//        }

//        //public async Task<MusicMoodResult> BuildMoodResultAsync()
//        //{
//        //    var bio = await _fitbitService.GetBiometricDataAsync();

//        //    var mood = _mappingService.ClassifyMood(bio);

//        //    var result = new MusicMoodResult
//        //    {
//        //        BiometricSummary = bio,
//        //        Mood = mood,
//        //        GeneratedAt = DateTime.Now
//        //    };

//        //    // 🔥 THIS PART IS MISSING IN YOUR FLOW
//        //    var user = await _userContext.GetCurrentUserAsync();

//        //    if (user != null)
//        //    {
//        //        await _calibrationRepo.AddAsync(new Calibration
//        //        {
//        //            UserID = user.ID,
//        //            RestingHeartRate = bio.AverageRestingHeartRate,
//        //            HRV = bio.AverageHeartRateVariability,
//        //            BreathingRate = bio.AverageBreathingRate,
//        //            Mood = mood.ToString(),
//        //            CreatedAt = DateTime.Now
//        //        });
//        //    }

//        //    return result;
//        //}

//        public async Task<MusicMoodResult> GenerateMoodMusicAsync()
//        {
//            return await BuildMoodResultAsync();
//        }

//        public async Task<MusicMoodResult> RecalibrateAsync()
//        {
//            var result = await BuildMoodResultAsync();

//            await SaveCalibrationAsync(result);

//            return result;
//        }

//        /// <summary>
//        /// The 
//        /// </summary>
//        /// <returns></returns>
//        private static async Task<DateTime> GetLastCalibrationDate()
//        {
//            // TEMP placeholder until database integration
//            await Task.CompletedTask;

//            return DateTime.Now.AddDays(-20);
//        }

//        private static (double danceability, double energy, double valence, double liveness) MapMoodToSpotify(MoodState mood)
//        {
//            return mood switch
//            {
//                MoodState.Calm => (0.4, 0.3, 0.6, 0.2),
//                MoodState.Active => (0.8, 0.8, 0.7, 0.5),
//                MoodState.Restless => (0.3, 0.4, 0.3, 0.3),
//                _ => (0.5, 0.5, 0.5, 0.5)
//            };
//        }

//        //private static CalibrationRecord MapToCalibration(BiometricSummary bio, int userId)
//        //{
//        //    return new CalibrationRecord
//        //    {
//        //        UserID = userId,
//        //        RestingHeartRate = bio.AverageRestingHeartRate,
//        //        HRV = bio.AverageHeartRateVariability,
//        //        BreathingRate = bio.AverageBreathingRate,
//        //        CreatedAt = DateTime.UtcNow
//        //    };
//        //}

//        /// <summary>
//        /// The 
//        /// </summary>
//        /// <param name="result"></param>
//        /// <returns></returns>
//        //private async Task SaveCalibrationAsync(MusicMoodResult result)
//        //{
//        //    var user = await _userContext.GetCurrentUserAsync();
//        //    if (user == null) return;

//        //    var record = new CalibrationRecord
//        //    {
//        //        UserID = user.ID,
//        //        RestingHeartRate = result.BiometricSummary!.AverageRestingHeartRate,
//        //        HRV = result.BiometricSummary!.AverageHeartRateVariability,
//        //        BreathingRate = result.BiometricSummary!.AverageBreathingRate,
//        //        Mood = result.Mood.ToString(),

//        //        TracksJson = JsonSerializer.Serialize(
//        //            result.RecommendedTracks?.Select(
//        //                t => new
//        //                {
//        //                    t.Name,
//        //                    Artist = t.Artists?.FirstOrDefault()?.Name
//        //                })
//        //            ),
//        //        CreatedAt = DateTime.UtcNow
//        //    };

//        //    await _calibrationRepo.AddAsync(record);
//        //}
//        private async Task SaveCalibrationAsync(MusicMoodResult result)
//        {
//            var user = await _userContext.GetCurrentUserAsync();

//            if (user == null)
//            {
//                Console.WriteLine("SaveCalibration: user is NULL");
//                return;
//            }

//            if (result.BiometricSummary == null)
//            {
//                Console.WriteLine("SaveCalibration: biometric data is NULL");
//                return;
//            }

//            var record = new CalibrationRecord
//            {
//                UserID = user.ID,
//                RestingHeartRate = result.BiometricSummary.AverageRestingHeartRate,
//                HRV = result.BiometricSummary.AverageHeartRateVariability,
//                BreathingRate = result.BiometricSummary.AverageBreathingRate,
//                Mood = result.Mood.ToString(),

//                TracksJson = JsonSerializer.Serialize(
//                    result.RecommendedTracks?
//                        .Select(t => new
//                        {
//                            t.Name,
//                            Artist = t.Artists?.FirstOrDefault()?.Name
//                        })
//                   .ToList()
//                ),

//                CreatedAt = DateTime.UtcNow
//            };

//            Console.WriteLine($"Saving calibration for user {user.ID}");

//            await _calibrationRepo.AddAsync(record);
//        }
//    }
//}

using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;
using MusicBased_IOT_Platform.Application.Interfaces.Flask;
using MusicBased_IOT_Platform.Application.Interfaces.Spotify;
using MusicBased_IOT_Platform.Application.Services.Flask;
using MusicBased_IOT_Platform.Models;
using System.Text.Json;

namespace MusicBased_IOT_Platform.Application.Services
{
    /// <summary>
    /// The constructor for the MoodCalibrationService initializes the necessary services for
    /// </summary>
    /// <param name="fitbitDataService"></param>
    /// <param name="spotifyDataService"></param>
    //public class MoodCalibrationService( IFitbitDataService live, IFitbitDataService fallback)
    public class MoodCalibrationService(IFlaskDataService flaskDataService, IFitbitDataService fitbitDataService, ISpotifyDataService spotifyDataService, IUserContext userContext, ICalibrationRepository calibrationRepository)
    {
        private readonly IFlaskDataService _flaskDataService = flaskDataService;
        private readonly IFitbitDataService _fitbitDataService = fitbitDataService;
        private readonly ISpotifyDataService _spotifyDataService = spotifyDataService;

        private readonly IUserContext _userContext = userContext;
        private readonly ICalibrationRepository _calibrationRepo = calibrationRepository;

        /// <summary>
        /// The MoodCalibrationService is responsible for determining if a recalibration 
        /// of the user's mood is necessary based on their biometric data and providing 
        /// music recommendations accordingly.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> ShouldRecalibrateAsync()
        {
            var user = await _userContext.GetCurrentUserAsync() ?? throw new InvalidOperationException("User not loaded before recalibration check.");
            var records = await _calibrationRepo.GetRecentAsync(user.ID, 14);

            if (records == null || records.Count == 0)
            {
                Console.WriteLine("No calibration records found.");
                return true;
            }

            var lastCalibration = records
                .OrderByDescending(r => r.CreatedAt)
                .First();

            return (DateTime.UtcNow - lastCalibration.CreatedAt).TotalDays >= 14;
        }

        /// <summary>
        /// The RecalibrateAsync method retrieves the latest biometric data from the Fitbit service,
        /// </summary>
        /// <returns></returns>
        public async Task<MusicMoodResult> BuildMoodResultAsync()
        {
            //try catch?????

            //  var biometric = await _fitbitDataService.GetBiometricDataAsync();

            BiometricSummary biometric;

            try
            { 
                biometric = await _flaskDataService.GetBiometricDataAsync();
            }

            catch(Exception ex)
            {
                Console.WriteLine("Flask failed: " + ex.Message);

                biometric = await _fitbitDataService.GetBiometricDataAsync();
            }

            var baseline = await GetBaselineAsync();

            var mood = MoodClassifier.ClassifyMood(biometric, baseline);

           // var biometric = await _flaskDataService.GetBiometricDataAsync();
            //var mood = MoodClassifier.ClassifyMood(biometric);

            var (danceability, energy, valence, liveness) = MapMoodToSpotify(mood);

            var tracks = await _spotifyDataService.GetMoodRecommendations(
                10,
                danceability,
                energy,
                valence,
                liveness);

            var result = new MusicMoodResult
            {
                BiometricSummary = biometric,
                Mood = mood,
                RecommendedTracks = tracks?.Tracks?.ToList() ?? [],
                GeneratedAt = DateTime.UtcNow
            };

            var user = await _userContext.GetCurrentUserAsync();

            if (user != null)
            {
                await _calibrationRepo.AddAsync(new CalibrationRecord
                {
                    UserID = user.ID,
                    RestingHeartRate = biometric.AverageRestingHeartRate,
                    HRV = biometric.AverageHeartRateVariability,
                    BreathingRate = biometric.AverageBreathingRate,
                    Mood = mood.ToString(),
                    TracksJson = JsonSerializer.Serialize(
                        result.RecommendedTracks?
                            .Select(t => new
                            {
                                t.Name,
                                Artist = t.Artists?.FirstOrDefault()?.Name
                            })),
                    CreatedAt = DateTime.UtcNow
                });
            }
            return result;
        }

        //public async Task<MusicMoodResult> BuildMoodResultAsync()
        //{
        //    var bio = await _fitbitService.GetBiometricDataAsync();

        //    var mood = _mappingService.ClassifyMood(bio);

        //    var result = new MusicMoodResult
        //    {
        //        BiometricSummary = bio,
        //        Mood = mood,
        //        GeneratedAt = DateTime.Now
        //    };

        //    // 🔥 THIS PART IS MISSING IN YOUR FLOW
        //    var user = await _userContext.GetCurrentUserAsync();

        //    if (user != null)
        //    {
        //        await _calibrationRepo.AddAsync(new Calibration
        //        {
        //            UserID = user.ID,
        //            RestingHeartRate = bio.AverageRestingHeartRate,
        //            HRV = bio.AverageHeartRateVariability,
        //            BreathingRate = bio.AverageBreathingRate,
        //            Mood = mood.ToString(),
        //            CreatedAt = DateTime.Now
        //        });
        //    }

        //    return result;
        //}

        public async Task<MusicMoodResult> GenerateMoodMusicAsync()
        {
            return await BuildMoodResultAsync();
        }

        //public async Task<MusicMoodResult> RecalibrateAsync()
        //{
        //    var result = await BuildMoodResultAsync();

        //    await SaveCalibrationAsync(result);

        //    return result;
        //}

        //public async Task<MusicMoodResult> RecalibrateAsync()
        //{
        //    var result = await BuildMoodResultAsync();

        //    await SaveCalibrationAsync(result);

        //    return result;
        //}

        public async Task RecalibrateAsync()
        {
            var user = await _userContext.GetCurrentUserAsync();

            var biometric = await _flaskDataService.GetBiometricDataAsync();

            var record = new CalibrationRecord
            {
                UserID = user!.ID,
                RestingHeartRate = biometric.AverageRestingHeartRate,
                HRV = biometric.AverageHeartRateVariability,
                BreathingRate = biometric.AverageBreathingRate,
                CreatedAt = DateTime.UtcNow
            };
            //    await SaveCalibrationAsync(result);


            await _calibrationRepo.AddAsync(record);
        }

        public async Task<CalibrationRecord?> GetBaselineAsync()
        {
            var user = await _userContext.GetCurrentUserAsync();

            var records = await _calibrationRepo.GetRecentAsync(user!.ID, 14);

            if (records == null || records.Count == 0)
                return null;

            return new CalibrationRecord
            {
                RestingHeartRate = records.Average(r => r.RestingHeartRate),
                HRV = records.Average(r => r.HRV),
                BreathingRate = records.Average(r => r.BreathingRate)
            };
        }

        public static MoodState ClassifyMood(
    BiometricSummary current,
    CalibrationRecord? baseline)
        {
            if (baseline == null)
            {
                return MoodState.Neutral;
            }

            var hrDiff = current.AverageRestingHeartRate - baseline.RestingHeartRate;
            var hrvDiff = current.AverageHeartRateVariability - baseline.HRV;

            if (hrDiff > 10 && hrvDiff < -10)
                return MoodState.Stressed;

            if (hrDiff < -5 && hrvDiff > 5)
                return MoodState.Calm;

            if (current.AverageActiveMinutes > baseline.RestingHeartRate)
                return MoodState.Active;

            return MoodState.Neutral;
        }

        /// <summary>
        /// The 
        /// </summary>
        /// <returns></returns>
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

        private async Task SaveCalibrationAsync(MusicMoodResult result)
        {
            var user = await _userContext.GetCurrentUserAsync();

            if (user == null)
            {
                Console.WriteLine("SaveCalibration: user is NULL");
                return;
            }

            if (result.BiometricSummary == null)
            {
                Console.WriteLine("SaveCalibration: biometric data is NULL");
                return;
            }

            var record = new CalibrationRecord
            {
                UserID = user.ID,
                RestingHeartRate = result.BiometricSummary.AverageRestingHeartRate,
                HRV = result.BiometricSummary.AverageHeartRateVariability,
                BreathingRate = result.BiometricSummary.AverageBreathingRate,
                Mood = result.Mood.ToString(),

                TracksJson = JsonSerializer.Serialize(
                    result.RecommendedTracks?
                        .Select(t => new
                        {
                            t.Name,
                            Artist = t.Artists?.FirstOrDefault()?.Name
                        })
                   .ToList()
                ),

                CreatedAt = DateTime.UtcNow
            };

            Console.WriteLine($"Saving calibration for user {user.ID}");

            await _calibrationRepo.AddAsync(record);
        }
    }
}
