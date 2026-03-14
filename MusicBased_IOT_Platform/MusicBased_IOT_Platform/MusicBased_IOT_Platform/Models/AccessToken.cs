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
            RefreshToken = string.Empty;
        }

        // Properties

        /// <summary>
        /// DateTimeAcquired
        /// </summary>
        public DateTime DateTimeAcquired { get; set; }

        /// <summary>
        /// The expires_in field indicates the lifetime in seconds of the access token. 
        /// </summary>
        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; } // Period of time in seconds

        /// <summary>
        /// Token
        /// </summary>
        [JsonPropertyName("access_token")]
        public string Token { get; set; }

        /// <summary>
        /// The refresh_token field is a token that can be used to obtain a new access token. 
        /// It is only returned for certain authorization flows and when the access token expires in less than 60 days. 
        /// For more information, see the Spotify Web API Authorization Guide.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The token_type field indicates the type of token returned.
        /// </summary>
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        // Methods

        /// <summary>
        /// The ToString method is overridden to provide a string representation of the AccessToken object,
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            var refreshPreview = string.IsNullOrEmpty(RefreshToken)
        ? "none"
        : string.Concat(RefreshToken.AsSpan(0, Math.Min(6, RefreshToken.Length)), "...");

            return $"Access token: {Token}, Type: {TokenType}, Acquired: {DateTimeAcquired}, Expires in: {ExpiresIn}, Refresh Token: {refreshPreview }";
        }

    }


}
