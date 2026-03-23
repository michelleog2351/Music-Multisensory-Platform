using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces
{
    public interface ICalibrationRepository
    {
        Task AddAsync(CalibrationRecord record);
    }
}
