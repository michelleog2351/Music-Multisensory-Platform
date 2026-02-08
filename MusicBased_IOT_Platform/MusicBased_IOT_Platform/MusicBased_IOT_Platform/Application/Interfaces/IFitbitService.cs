using Microsoft.AspNetCore.Connections.Features;

namespace MusicBased_IOT_Platform.Application.Interfaces
{
    public interface IFitbitService
    {

        public IConnectionHeartbeatFeature GetHeartbeatFeature();

        static double ReadBiometricData()
        {
            throw new NotImplementedException();
        }

        static double ReadHeartRateVariabilityData() { 
            throw new NotImplementedException(); 
        }
        
        static double BreathingRateData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The Distance_In_Steps method
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static double Distance_In_Steps()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The <c>TestDataConnection</c> method tests if the client has a valid data connection
        /// to the data service. 
        /// </summary>
        /// <returns><c>True</c> if it has a valid data connection, otherwise <c>false</c>.</returns>
        public bool TestDataConnection();
    }
}
