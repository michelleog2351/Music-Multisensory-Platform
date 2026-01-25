// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace MusicBased_IOT_Platform.Models
{
    /// <summary>
    /// The Genre class stores genres
    /// </summary>
    public class Genre
    {
        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("genres")]
        public List<string>? Genres { get; set; }
    }
}
