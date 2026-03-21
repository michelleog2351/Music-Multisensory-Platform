// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

namespace MusicBased_IOT_Platform.Models
{
    /// <summary>
    /// The AppSettings class is used to store authentication details
    /// such as ClientID and ClientSecret
    /// </summary>
    public class FitbitSettings
    {

        // Constructors
        /// <summary>
        /// Constructor for FitbitSettings class
        /// </summary>
        public FitbitSettings()
        {
            // Set default settings for the application
            AuthorisationUrl = string.Empty;
            BaseURL = "https://api.fitbit.com";
            ClientID = string.Empty;
            ClientSecret = string.Empty;
            RedirectUri = string.Empty;
        }

        // Properties

        /// <summary>
        /// The URL Fitbit uses to authorise clients and get access token
        /// </summary>
        public string AuthorisationUrl { get; set; }

        /// <summary>
        /// The BaseURL is core URL for the live market client to retrieve live market data. 
        /// </summary>
        public string BaseURL { get; set; }

        /// <summary>
        /// The ClientSecret string used to get an API token from the spotify service
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// The ClientID string used to get an API token from the spotify service
        /// </summary>
        public string? ClientID { get; set; }

        public string RedirectUri { get; set; }

        /// <summary>
        /// The Scopes list contains the scopes of access that the application is requesting from the Spotify API.
        /// </summary>
        public List<string> Scopes { get; set; } = [];
    }
}
