using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces
{
    public interface ICalibrationRepository
    {
        Task AddAsync(CalibrationRecord record);

        Task<List<CalibrationRecord>> GetByUserIDAsync(int userID);

        Task<List<CalibrationRecord>> GetRecentAsync(int userId, int days);
    }
}
