using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Data;
using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Repository
{
    public class CalibrationRepository : ICalibrationRepository
    {
        private readonly AppDBContext _dB;

        public CalibrationRepository(AppDBContext dbContext)
        {
            _dB = dbContext;
        }

        public async Task AddAsync(CalibrationRecord record)
        {
            _dB.Calibrations.Add(record);
            await _dB.SaveChangesAsync();
        }
    }
}
