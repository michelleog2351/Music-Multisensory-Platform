// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicBased_IOT_Platform.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// The NewReleases class stores a list of new Releases
    /// </summary>
    public class NewReleases
    {
        /// <summary>
        /// Albums
        /// </summary>
        [JsonPropertyName("albums")]
        public Albums? Albums { get; set; }
    }

    /// <summary>
    /// The Albums class stores Albums
    /// </summary>
    public class Albums
    {
        /// <summary>
        /// Href
        /// </summary>
        [JsonPropertyName("href")]
        public string? Href { get; set; }

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
    /// The Item class stores recommendations Item
    /// </summary>
    public class Item
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
        /// Images
        /// </summary>
        [JsonPropertyName("images")]
        public List<Image>? Images { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

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
        public int TotalTracks { get; set; }

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
