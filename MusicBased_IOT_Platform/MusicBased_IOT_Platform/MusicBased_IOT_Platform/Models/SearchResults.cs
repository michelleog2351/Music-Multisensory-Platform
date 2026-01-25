// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;

namespace MusicBased_IOT_Platform.Models
{

    /// <summary>
    /// The SearchResults class is used to store all possible results from a search.
    /// A search result may return a combination of Albums, Artists and Tracks
    /// See: https://developer.spotify.com/documentation/web-api/reference/search
    /// </summary>
    public class SearchResults
    {
        /// <summary>
        /// Albums
        /// </summary>
        [JsonPropertyName("albums")]
        public SearchResultAlbums? Albums { get; set; }

        /// <summary>
        /// Artists
        /// </summary>
        [JsonPropertyName("artists")]
        public SearchResultArtists? Artists { get; set; }

        /// <summary>
        /// Tracks
        /// </summary>
        [JsonPropertyName("tracks")]
        public SearchResultTracks? Tracks { get; set; }

    }

    /// <summary>
    /// A POCO class used to store search results relating to albums
    /// Inherits from Album
    /// </summary>
    public class SearchResultAlbums : Album
    {
        /// <summary>
        /// Tracks
        /// </summary>
        [JsonPropertyName("items")]
        public List<Item>? Items { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Next
        /// </summary>
        [JsonPropertyName("next")]
        public string? Next { get; set; }

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
    /// POCO class to store search results relating to artists
    /// Inherits from Artists
    /// </summary>
    public class SearchResultArtists : Artist
    {
        /// <summary>
        /// Type
        /// </summary>
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public List<Item>? Items { get; set; }

        /// <summary>
        /// Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }

        /// <summary>
        /// Next
        /// </summary>
        [JsonPropertyName("next")]
        public string? Next { get; set; }

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
    /// A POCO class to store search results relating to tracks
    /// Inherits from Track
    /// </summary>
    public class SearchResultTracks : Track
    {

        /// <summary>
        /// Items
        /// </summary>
        [JsonPropertyName("items")]
        public List<Item>? Items { get; set; }


        /// <summary>
        /// Limit
        /// </summary>
        [JsonPropertyName("limit")]
        public int Limit { get; set; }


        /// <summary>
        /// Next
        /// </summary>
        [JsonPropertyName("next")]
        public string? Next { get; set; }


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
    /// Search results may consist of albums, artists and tracks. As a result the SearchResultItem
    /// could contain a variety of information related to an artist, track or album. Some fields
    /// are likely to be null.
    /// </summary>
    public class SearchResultItem : Item
    {
        /// <summary>
        /// IsPlayable
        /// </summary>
        [JsonPropertyName("is_playable")]
        public bool IsPlayable { get; set; }

        /// <summary>
        /// Restrictions
        /// </summary>
        [JsonPropertyName("restrictions")]
        public Restrictions? Restrictions { get; set; }

        /// <summary>
        /// Followers
        /// </summary>
        [JsonPropertyName("followers")]
        public Followers? Followers { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("genres")]
        public List<string>? Genres { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("album")]
        public Album? Album { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("disc_number")]
        public int DiscNumber { get; set; }

        /// <summary>
        /// Genres
        /// </summary>
        [JsonPropertyName("duration_ms")]
        public int DurationMs { get; set; }

        /// <summary>
        /// Explicit
        /// </summary>
        [JsonPropertyName("explicit")]
        public bool Explicit { get; set; }

        /// <summary>
        /// ExternalIds
        /// </summary>
        [JsonPropertyName("external_ids")]
        public ExternalIds? ExternalIds { get; set; }

        /// <summary>
        /// IsLocal
        /// </summary>
        [JsonPropertyName("is_local")]
        public bool IsLocal { get; set; }

        /// <summary>
        /// PreviewUrl
        /// </summary>
        [JsonPropertyName("preview_url")]
        public string? PreviewUrl { get; set; }

        /// <summary>
        /// TrackNumber
        /// </summary>
        [JsonPropertyName("track_number")]
        public int TrackNumber { get; set; }
    }

    /// <summary>
    /// Restrictions stores restriction details
    /// </summary>
    public class Restrictions
    {
        /// <summary>
        /// Reason
        /// </summary>
        [JsonPropertyName("reason")]
        public string? Reason { get; set; }
    }



}
