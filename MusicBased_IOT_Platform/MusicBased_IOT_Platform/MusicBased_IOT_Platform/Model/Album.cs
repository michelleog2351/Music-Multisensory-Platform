// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// A POCO class to store a list of albums.
    /// </summary>
    public class ListAlbums
    {
        /// <summary>
        /// Albums
        /// </summary>
        [JsonPropertyName("albums")]
        public List<Album>? Albums { get; set; }
    }

    /// <summary>
    /// The Album class is used to store all possible results of an album.
    /// </summary>
    public class Album
    {
        /// <summary>
        /// AlbumType
        /// </summary>
        [JsonPropertyName("album_type")]
        public string? AlbumType { get; set; }

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
        /// Copyrights
        /// </summary>
        [JsonPropertyName("copyrights")]
        public List<Copyright>? Copyrights { get; set; }

        /// <summary>
        /// ExternalIds
        /// </summary>
        [JsonPropertyName("external_ids")]
        public ExternalIds? ExternalIds { get; set; }

        /// <summary>
        /// ExternalUrls
        /// </summary>
        [JsonPropertyName("external_urls")]
        public ExternalUrls? ExternalUrls { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("genres")]
        public List<object>? Genres { get; set; }

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
        /// Images
        /// </summary>
        [JsonPropertyName("images")]
        public List<Image>? Images { get; set; }

        /// <summary>
        /// Label
        /// </summary>
        [JsonPropertyName("label")]
        public string? Label { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Popularity
        /// </summary>
        [JsonPropertyName("popularity")]
        public int? Popularity { get; set; }

        /// <summary>
        /// ReleaseDate
        /// </summary>
        [JsonPropertyName("release_date")]
        public string? ReleaseDate { get; set; }

        /// <summary>
        /// ReleaseDatePrecision
        /// </summary>
        [JsonPropertyName("release_date_precision")]
        public string? ReleaseDatePrecision { get; set; }

        /// <summary>
        /// TotalTracks
        /// </summary>
        [JsonPropertyName("total_tracks")]
        public int? TotalTracks { get; set; }

        /// <summary>
        /// Tracks
        /// </summary>
        [JsonPropertyName("tracks")]
        public AlbumTracks? Tracks { get; set; }

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
    }

    /// <summary>
    /// A POCO class to store a tracks of albums.
    /// </summary>
    public class AlbumTracks
    {
        /// <summary>
        /// Href
        /// </summary>
        [JsonPropertyName("href")]
        public string? Href { get; set; }

        /// <summary>
        /// Tracks
        /// </summary>
        [JsonPropertyName("items")]
        public List<Track>? Tracks { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Next
        /// </summary>
        [JsonPropertyName("next")]
        public object? Next { get; set; }

        /// <summary>
        /// Offset
        /// </summary>
        [JsonPropertyName("offset")]
        public int Offset { get; set; }

        /// <summary>
        /// Previous
        /// </summary>
        [JsonPropertyName("previous")]
        public object? Previous { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    /// <summary>
    /// A POCO class to store a items of albums.
    /// </summary>
    public class AlbumItem
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
    }
}
