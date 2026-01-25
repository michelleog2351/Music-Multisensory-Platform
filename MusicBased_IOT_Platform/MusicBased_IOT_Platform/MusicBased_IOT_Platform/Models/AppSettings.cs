// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicBased_IOT_Platform.Models
{
    /// <summary>
    /// The AppSettings class is used to store authentication details
    /// such as ClientID and ClientSecret
    /// </summary>
    public class AppSettings
    {

        // Constructors
        /// <summary>
        /// Constructor for AppSettings class
        /// </summary>
        public AppSettings()
        {
            // Set default settings for the application
            AuthorisationUrl = string.Empty;
            BaseURL = string.Empty;
            ClientID = string.Empty;
            ClientSecret = string.Empty;
            InTest = true;
            InDevelopment = true;
        }

        // Properties

        /// <summary>
        /// The URL Spotify uses to authorise clients and get access token
        /// </summary>
        public string AuthorisationUrl { get; set; }

        /// <summary>
        /// If InDevelopment is true the code is not deployed to production. 
        /// </summary>
        public bool InDevelopment { get; set; }

        /// <summary>
        /// If InTest is true we are using the MockClient to get asset quotes.
        /// </summary>
        public bool InTest { get; set; }

        /// <summary>
        /// The BaseURL is core URL for the live market client to retrieve live market data. 
        /// </summary>
        public string BaseURL { get; set; }

        /// <summary>
        /// The ClientID string used to get an API token from the spotify service
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The ClientSecret string used to get an API token from the spotify service
        /// </summary>
        public string? ClientID { get; set; }

    }
}
