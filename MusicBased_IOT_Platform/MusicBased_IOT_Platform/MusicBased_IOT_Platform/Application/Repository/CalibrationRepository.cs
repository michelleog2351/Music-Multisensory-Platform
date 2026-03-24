using Microsoft.EntityFrameworkCore;
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

        public async Task<List<CalibrationRecord>> GetByUserIDAsync(int userID)
        {
            return await _dB.Calibrations
                .Where(c => c.UserID == userID)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<CalibrationRecord>> GetRecentAsync(int userId, int days)
        {
            return await _dB.Calibrations
                .Where(x => x.UserID == userId &&
                            x.CreatedAt >= DateTime.UtcNow.AddDays(-days))
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
