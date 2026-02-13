using Microsoft.AspNetCore.Connections.Features;

namespace MusicBased_IOT_Platform.Application.Interfaces.Fitbit
{
    public interface IFitbitService
    {

        /// <summary>
        /// The 
        /// </summary>
        /// <returns></returns>
        public IConnectionHeartbeatFeature GetHeartbeatFeature();

        /// <summary>
        /// The
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>

        //static double HeartRateSummary GetDailyHeartRate(DateTime date);

        /// <summary>
        /// The
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        // SleepSummary GetSleep(DateTime date);


        /// <summary>
        /// The
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static double ReadBiometricData()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        static double ReadHeartRateVariabilityData() { 
            throw new NotImplementedException(); 
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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
