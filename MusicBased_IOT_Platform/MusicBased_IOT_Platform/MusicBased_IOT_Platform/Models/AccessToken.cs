// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicBased_IOT_Platform.Models
{
    /// <summary>
    /// A POCO class to store the access token acquired from the Spotify authentication process.
    /// </summary>
    public class AccessToken
    {
        // Fields

        // Constructors
        /// <summary>
        /// Default constructor for the AccessToken initialising string properties to empty
        /// strings.
        /// </summary>
        public AccessToken()
        {
            DateTimeAcquired = DateTime.Now;
            Token = string.Empty;
            TokenType = string.Empty;
        }

        // Properties

        /// <summary>
        /// DateTimeAcquired
        /// </summary>
        public DateTime DateTimeAcquired { get; set; }

        /// <summary>
        /// ExpiresIn
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; } // Period of time in seconds

        /// <summary>
        /// Token
        /// </summary>
        [JsonPropertyName("access_token")]
        public string Token { get; set; }

        /// <summary>
        /// TokenType
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        // Methods

        /// <summary>
        /// ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Access token: {Token}, Type: {TokenType}, Acquired: {DateTimeAcquired}, Expires in: {ExpiresIn}";
        }

    }


}
