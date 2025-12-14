// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicBased_IOT_Platform.Model;
using System.Text.Json.Serialization;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// A POCO class used to store track information
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Artists
        /// </summary>
        [JsonPropertyName("artists")]
        public List<Artist>? Artists { get; set; }

        /// <summary>
        /// AvailableMarkets
        /// </summary>
        [JsonPropertyName("available_markets")]
        public List<string>? AvailableMarkets { get; set; }

        /// <summary>
        /// DiscNumber
        /// </summary>
        [JsonPropertyName("disc_number")]
        public int DiscNumber { get; set; }

        /// <summary>
        /// DurationMs
        /// </summary>
        [JsonPropertyName("duration_ms")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Explicit
        /// </summary>
        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        /// <summary>
        /// ExternalUrls
        /// </summary>
        [JsonPropertyName("external_urls")]
        public ExternalUrls? ExternalUrls { get; set; }

        /// <summary>
        /// Href
        /// </summary>
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        /// <summary>
        /// IsLocal
        /// </summary>
        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// PreviewUrl
        /// </summary>
        [JsonPropertyName("preview_url")]
        public object? PreviewUrl { get; set; }

        /// <summary>
        /// TrackNumber
        /// </summary>
        [JsonPropertyName("track_number")]
        public int TrackNumber { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Uri
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

        /// <summary>
        /// Popularity
        /// </summary>
        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        /// <summary>
        /// Album
        /// </summary>
        [JsonPropertyName("album")]
        public Album? Album { get; set; }
    }

    /// <summary>
    /// A POCO class used to store a list of tracks when the get several tracks endpoint is queried
    /// See: https://developer.spotify.com/documentation/web-api/reference/get-several-tracks
    /// </summary>
    public class Tracks
    {
        /// <summary>
        /// TrackList
        /// </summary>
        [JsonPropertyName("tracks")]
        public List<Track>? TrackList { get; set; }
        
    }
}
