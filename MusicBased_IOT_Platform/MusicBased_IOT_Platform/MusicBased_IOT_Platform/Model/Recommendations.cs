// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// The Recommendations class stores recommendations lists
    /// </summary>
    public class Recommendations
    {
        /// <summary>
        /// Tracks
        /// </summary>
        [JsonPropertyName("tracks")]
        public List<Track>? Tracks { get; set; }
        /// <summary>
        /// Artists
        /// </summary>
        [JsonPropertyName("artists")]
        public List<Artist>? Artists { get; set; }
        /// <summary>
        /// Seeds
        /// </summary>
        [JsonPropertyName("seeds")]
        public List<Seed>? Seeds { get; set; }
    }

    /// <summary>
    /// The Seed class stores seed data
    /// </summary>
    public class Seed
    {
        /// <summary>
        /// InitialPoolSize
        /// </summary>
        [JsonPropertyName("initialPoolSize")]
        public int InitialPoolSize { get; set; }

        /// <summary>
        /// AfterFilteringSize
        /// </summary>
        [JsonPropertyName("afterFilteringSize")]
        public int AfterFilteringSize { get; set; }

        /// <summary>
        /// AfterRelinkingSize
        /// </summary>
        [JsonPropertyName("afterRelinkingSize")]
        public int AfterRelinkingSize { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Href
        /// </summary>
        [JsonPropertyName("href")]
        public string? Href { get; set; }
    }
}
