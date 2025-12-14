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
