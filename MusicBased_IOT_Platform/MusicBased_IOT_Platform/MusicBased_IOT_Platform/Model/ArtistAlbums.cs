// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Text.Json.Serialization;


namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// The ArtistAlbums class is used to store an artists albums.
    /// </summary>
    public class ArtistAlbums
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
        public List<Album>? Items { get; set; }


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
}
