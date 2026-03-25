/* 
 * Filename: FitbitApplicationExceptions.cs
 * Description: Contains custom exceptions from the Fitbit Web API integration.
 */

namespace MusicBased_IOT_Platform.Exceptions
{
    public class FitbitApplicationException
    {
        /// <summary>
        /// A custom exception to throw when the application cannot authenticate with the Fitbit
        /// authentication service and acquire a valid access token.
        /// </summary>
        [Serializable]
        public class FitbitServiceConnectionException : Exception
        {
            /// <summary>
            /// FitbitServiceConnectionException
            /// </summary>
            public FitbitServiceConnectionException() : base() { }
            /// <summary>
            /// FitbitServiceConnectionException
            /// </summary>
            /// <param name="message"></param>
            public FitbitServiceConnectionException(string message) : base(message) { }
        }
    }
}
