// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Security.Cryptography.X509Certificates;

namespace MusicBased_IOT_Platform.Models
{
    /// <summary>
    /// The AppSettings class is used to store authentication details
    /// such as ClientID and ClientSecret
    /// </summary>
    public class SpotifySettings
    {

        // Constructors
        /// <summary>
        /// Constructor for AppSettings class
        /// </summary>
        public SpotifySettings()
        {
            // Set default settings for the application
            AuthorisationUrl = string.Empty;
            BaseURL = "https://api.spotify.com/v1";
            ClientID = string.Empty;
            ClientSecret = string.Empty;
            RedirectUri = "https://localhost:7039/signin-spotify";
        }

        // Properties

        /// <summary>
        /// The URL Spotify uses to authorise clients and get access token
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

        /// <summary>
        /// The <c>RedirectUri</c>
        /// </summary>
        public string RedirectUri { get; set; }

        /// <summary>
        /// The Scopes list contains the scopes of access that the application is requesting from the Spotify API.
        /// </summary>
        public List<string> Scopes { get; set; } = [];
    }
}
