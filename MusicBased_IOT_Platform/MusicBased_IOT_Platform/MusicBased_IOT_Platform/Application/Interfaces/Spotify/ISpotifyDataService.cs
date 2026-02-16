// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.


// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using MusicBased_IOT_Platform.Models;

namespace MusicBased_IOT_Platform.Application.Interfaces.Spotify
{
    /// <summary>
    /// This interface represents defines the base functionality that the dataClients
    /// (live and mock) must implement. 
    /// </summary>
    public interface ISpotifyDataService
    {

        /// <summary>
        /// The <c>GetAlbum</c> method gets Spotify catalog information for a single album.
        /// </summary>
        /// <param name="id">The Spotify ID of the album.</param>
        /// <param name="market"></param>
        /// <returns>An album</returns>
        Task<Album> GetAlbum(string id, string market = "IE");

        /// <summary>
        /// The <c>GetAlbums</c> method gets the details of one or more albums with the specified IDs and optional market.
        /// </summary>
        /// <param name="ids">a comma-separated list of spotify IDs for the albums.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns>An instance of the <c>album</c> class</returns>
        Task<List<Album>> GetAlbums(string ids, string market = "IE");

        /// <summary>
        /// The <c>GetAlbumTracks</c> method returns a list of track objects containing the details
        /// of the tracks on the album. 
        /// </summary>
        /// <param name="ids">a <c>string</c> representing the ID of the album</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">An optional int specifying the number of items to return.</param>
        /// <returns>A list of tracks containing track details on the album</returns>
        Task<List<Track>> GetAlbumTracks(string ids, string market = "IE", int limit = 20);

        /// <summary>
        /// Get Spotify catalog information for a single artist identified by
        /// their unique Spotify ID.
        /// </summary>
        /// <param name = "id" > the Spotify IDs for the artist.</param>
        /// <returns>An artist object</returns>
        Task<Artist> GetArtist(string id);

        /// <summary>
        /// Get Spotify catalog information for a list of artists identified
        /// by their unique Spotify IDs.
        /// </summary>
        /// <param name="ids"> a comma-separated list of the Spotify IDs for
        /// the artists.</param>
        /// /// <param name="market"></param>
        /// /// <param name="limit"></param>
        /// <returns>A list of one or more <c>Artist</c> objects.</returns>
        Task<List<Artist>> GetArtists(string ids, string market = "IE", int limit = 20);

        /// <summary>
        /// The <c>GetArtistsAlbums</c> method gets Spotify catalog information about an artist's albums.
        /// </summary>
        /// <param name="id"> a string spotify ID for the artist.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <param name="limit">An optional int specifying the number of items to return.</param>
        /// <returns>A list of ArtistAlbum objects</returns>
        Task<ArtistAlbums> GetArtistsAlbums(string id, string market = "IE", int limit = 20);

        /// <summary>
        /// The <c>GetArtistsTopTracks</c> gets Spotify catalog information about an artist's top tracks by country.
        /// </summary>
        /// <param name="id"> a string spotify ID for the artist.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns>a list of top tracks for the artist.</returns>
        Task<ArtistTopTracks> GetArtistsTopTracks(string id, string market = "IE");

        /// <summary>
        /// Get a list of new album releases
        /// </summary>
        /// <returns>A list of album objects representing new releases</returns>
        Task<NewReleases> GetNewAlbumReleases(int limit = 20, int offset = 0);

        /// <summary>
        /// The method <c>GetRelatedArtists</c> gets Spotify catalog information about artists
        /// similar to a given artist. Similarity is based on analysis of the Spotify community's
        /// listening history.
        /// </summary>
        /// <param name="id"> a string spotify ID for the artist.</param>
        /// <returns>A list of <c>Artist</c> objects.</returns>
        Task<List<Artist>> GetRelatedArtists(string id);

        /// <summary>
        /// The <c>Search</c> method gets Spotify catalog information about albums, artists,
        /// playlists, tracks, shows, episodes or audiobooks that match a keyword string.
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="searchItemTypes"></param>
        /// <returns></returns>
        Task<SearchResults> Search(string searchQuery, string searchItemTypes);


        /// <summary>
        /// The <c>GetTrack</c> method get Spotify catalog information for a single track
        /// identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id"> a spotify ID for the required track.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns>A track object.</returns>
        Task<Track> GetTrack(string id, string market = "IE");

        /// <summary>
        /// The <c>GetTracks</c> method get Spotify catalog information several tracks
        /// identified by its unique Spotify ID.
        /// </summary>
        /// <param name="ids"> a comma-separated list of spotify IDs for the required tracks.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns>A list of track objects.</returns>
        Task<List<Track>> GetTracks(string ids, string market = "IE");

        /// <summary>
        /// The <c>GetRecommendations</c> method gets a list of recommended tracks based available
        /// information for a given seed entity and matched against similar artists and tracks.
        /// A maximum of five seed values can be provided when making the call to the spotify
        /// service. It can be any combination of artists, genres or tracks.
        /// </summary>
        /// <param name="seedArtists">A comma separated list of Spotify IDs for seed artists.</param>
        /// <param name="seedGenres">A comma separated list of any genres in the set of available
        /// genre seeds.</param>
        /// <param name="seedTracks">A comma separated list of Spotify IDs for a seed track. Up to 5 seed values may be provided.</param>
        /// <param name="limit">The target size of the list of recommended tracks.</param>
        /// <param name="market">an optional ISO 3166-1 alpha-2 country code.</param>
        /// <returns><c>Recommendations</c> object</returns>
        Task<Recommendations> GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit = 10, string market = "IE");

        /// <summary>
        /// <c>GetRecommendedTracks</c> method gets recommended tracks based on user input
        /// </summary>
        /// <returns> <c>Recommendations</c> object with recommended tracks based on users mood</returns>
        Task<Recommendations> GetRecommendedTracks(string id);

        /// <summary>
        /// <c>GetSeedGenres</c> returns a list of grnres
        /// </summary>
        /// <returns><c>List string</c></returns>
        Task<List<string>> GetSeedGenres();

        /// <summary>
        /// The <c>GetMoodRecommendations</c> method is used to get return
        /// recommendations based on what the users current mood
        /// </summary>
        /// <param name="limit"><c>int</c> limit to the number of records returned</param>
        /// <param name="max_danceability"><c>double</c>value for danceability</param>
        /// <param name="max_energy"><c>double</c>value for energy</param>
        /// <param name="max_valence"><c>double</c>value for valence</param>
        /// <param name="max_liveness"><c>double</c>value for liveness</param>
        /// <returns> Reccomendations object with tracks based on users mood</returns>
        Task<Recommendations> GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness);


        /// <summary>
        /// The <c>TestDataConnection</c> method tests if the client has a valid data connection
        /// to the data service. 
        /// </summary>
        /// <returns><c>True</c> if it has a valid data connection, otherwise <c>false</c>.</returns>
        Task<bool> TestDataConnection();
    }
}
