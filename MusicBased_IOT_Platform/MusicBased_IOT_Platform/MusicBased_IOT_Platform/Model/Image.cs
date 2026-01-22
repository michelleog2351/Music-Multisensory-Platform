// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// The Genre class stores images
    /// </summary>
    public class Image
    {
        /// <summary>
        /// Height
        /// </summary>
        [JsonPropertyName("height")]
        public int Height { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        /// <summary>
        /// Width
        /// </summary>
        [JsonPropertyName("width")]
        public int Width { get; set; }
    }
}
