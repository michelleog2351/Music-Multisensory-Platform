// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using SpotifyApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MusicBased_IOT_Platform.Model
{
    /// <summary>
    /// The Artist class is used to store all possible results of an artist.
    /// </summary>
    public class Artist
    {
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
        /// Name
        /// </summary>
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        /// <summary>
        /// Uri
        /// </summary>
        [JsonPropertyName("uri")]
        public string? Uri { get; set; }

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
        /// Popularity
        /// </summary>
        [JsonPropertyName("popularity")]
        public int Popularity { get; set; }
    }
    /// <summary>
    /// A POCO class to store a List of artists.
    /// </summary>
    public class ArtistsList
    {
        /// <summary>
        /// Artists
        /// </summary>
        [JsonPropertyName("artists")]
        public List<Artist>? Artists { get; set; }
    }

    /// <summary>
    /// A POCO class to store Followers.
    /// </summary>
    public class Followers
    {
        /// <summary>
        /// Href
        /// </summary>
        [JsonPropertyName("href")]
        public object? Href { get; set; }

        /// <summary>
        /// Total
        /// </summary>
        [JsonPropertyName("total")]
        public int Total { get; set; }
    }

    /// <summary>
    /// A POCO class to store Artists ArtistTopTracks.
    /// </summary>
    public class ArtistTopTracks
    {
        /// <summary>
        /// TopTracks
        /// </summary>
        [JsonPropertyName("tracks")]
        public List<Track>? TopTracks { get; set; }

    }
}
