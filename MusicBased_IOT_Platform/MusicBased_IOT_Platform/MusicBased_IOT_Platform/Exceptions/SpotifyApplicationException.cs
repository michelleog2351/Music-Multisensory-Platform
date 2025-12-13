/* 
 * Filename: SpotifyApplicationExceptions.cs
 * Description: Contains custom exceptions for the Spotify-based music recommendation application.
 */

namespace MusicBased_IOT_Platform.Exceptions
{
    public class SpotifyApplicationException
    {

        /// <summary>
        /// A custom exception to throw when we the application cannot authenticate with the spotify
        /// authentication service and acquire a valid access token.
        /// </summary>
        [Serializable]
        public class SpotifyServiceConnectionException : Exception
        {

            /// <summary>
            /// SpotifyServiceConnectionException
            /// </summary>
            public SpotifyServiceConnectionException() : base() { }

            /// <summary>
            /// SpotifyServiceConnectionException
            /// </summary>
            /// <param name="message"></param>
            public SpotifyServiceConnectionException(string message) : base(message) { }

            /// <summary>
            /// SpotifyServiceConnectionException
            /// </summary>
            /// <param name="message"></param>
            /// <param name="inner"></param>
            public SpotifyServiceConnectionException(string message, Exception inner) : base(message, inner) { }

        }

        /// <summary>
        /// A custom exception to throw if the ATU spotify recommendation application excounters an
        /// error during initialiation and configuration. 
        /// </summary>
        public class ApplicationConfigurationException : Exception
        {
            /// <summary>
            /// ApplicationConfigurationException
            /// </summary>
            public ApplicationConfigurationException() : base() { }

            /// <summary>
            /// ApplicationConfigurationException
            /// </summary>
            /// <param name="message"></param>
            public ApplicationConfigurationException(string message) : base(message) { }

            /// <summary>
            /// ApplicationConfigurationException
            /// </summary>
            /// <param name="message"></param>
            /// <param name="inner"></param>
            public ApplicationConfigurationException(string message, Exception inner) : base(message, inner) { }
        }

    }
}


