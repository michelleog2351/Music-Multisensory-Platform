using Microsoft.AspNetCore.Connections.Features;
using MusicBased_IOT_Platform.Application.Interfaces;
using MusicBased_IOT_Platform.Application.Interfaces.Fitbit;

namespace MusicBased_IOT_Platform.Application.Services.Fitbit.Live
{
    public class LiveFitbitDataService : IFitbitService
    {
        public LiveFitbitDataService() 
        {
            
        }

        public IConnectionHeartbeatFeature GetHeartbeatFeature()
        {
            // Implement logic to retrieve heartbeat feature from Fitbit API
            throw new NotImplementedException();
        }

        public bool TestDataConnection()
        {
            throw new NotImplementedException();
        }

        IConnectionHeartbeatFeature IFitbitService.GetHeartbeatFeature()
        {
            throw new NotImplementedException();
        }

        //   public HeartRateSummary GetDailyHeartRate(DateTime date)


    }
}
