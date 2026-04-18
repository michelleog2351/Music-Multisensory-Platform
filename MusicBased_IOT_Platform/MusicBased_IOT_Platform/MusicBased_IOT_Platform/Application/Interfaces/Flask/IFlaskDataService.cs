using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces.Flask
{
    public interface IFlaskDataService
    {
        Task<BiometricSummary> GetBiometricDataAsync();
        // device id??
    }
}
