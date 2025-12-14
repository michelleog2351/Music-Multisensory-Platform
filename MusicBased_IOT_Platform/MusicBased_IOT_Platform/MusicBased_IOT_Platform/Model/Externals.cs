// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// The ExternalUrls class stores ExternalUrls info
    /// </summary>
    public class ExternalUrls
    {
        /// <summary>
        /// Spotify
        /// </summary>
        [JsonPropertyName("spotify")]
        public string? Spotify { get; set; }
    }

    /// <summary>
    /// The ExternalUrls class stores ExternalIds info
    /// </summary>
    public class ExternalIds
    {
        /// <summary>
        /// Upc
        /// </summary>
        [JsonPropertyName("upc")]
        public string? Upc { get; set; }
    }
}
