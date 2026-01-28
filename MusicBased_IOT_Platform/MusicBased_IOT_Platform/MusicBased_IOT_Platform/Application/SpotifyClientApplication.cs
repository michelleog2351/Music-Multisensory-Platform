// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

/* 
 * Filename: SpotifyClientApplication.cs
 * Description: Contains the definition of the SpotifyClientApplication class.
 */

using MusicBased_IOT_Platform.Models;
using MusicBased_IOT_Platform.Services;
using System.Diagnostics;
using System.Text;


namespace MusicBased_IOT_Platform.Application
{

    /// <summary>
    /// The class SpotifyClient defines the core functionality of the SpotifyClient. It uses an
    /// instance of the ConsoleUI to output data to the console and retrieve input. Data is aquired
    /// via an instance of the ISpotifyDataClient. 
    /// You should implement the methods specifeid. Add additional methods as necessary.
    /// </summary>
    public class SpotifyClientApplication
    {

        // Fields
        private ISpotifyService? _spotifyDataService;
        const int MainMenuMaxValue = 10;
        const int SearchMenuMaxValue = 3;
        const int FavouritesMenuMaxValue = 4;
        const int ConfirmMenuMaxValue = 5;
        const int OptionMenuMaxValue = 4;
        const int RecFileMenuMaxValue = 1;
        const int RecMenuMaxValue = 4;
        const int RecMoodMenuMaxValue = 4;



        //Lists to hold favoutites
        static List<Artist> favouriteArtists = [];
        static List<Track> favouriteTracks = [];
        static List<Album> favouriteAlbums = [];
        //Options class calls add favoutite method


        // Constructors
        /// <summary>
        /// SpotifyClientApplication() method
        /// </summary>
        public SpotifyClientApplication()
        {
            // Set the spotifyDataClient to the live service
            //_spotifyDataService = new MockSpotifyDataService(); //initialise mock spotify data service. we have two: Live and Mock
            AppConfig = new AppSettings();

            //LoadAppSettings("appSettings.json"); //live
            LoadAppSettings(@"../../Raw/appSettings.json"); //mock
            InitialiseApplication();
        }

        // Properties
        /// <summary>
        /// AppSettings getter and setter method
        /// </summary>
        public AppSettings AppConfig { get; set; }

        // Methods

        /// <summary>
        /// The <c>InitialiseApplication </c> method creates a spotifyDataService object depending
        /// on the configuration of the application. If we are in testing mode the application
        /// will create a mock spotify data service for testing. If we are not in testing the
        /// application will attempt to create an instance of the live spotify data service. If
        /// the application cannot be initialised successfully then we will inform the user and
        /// terminate the application.
        /// </summary>
        private void InitialiseApplication()
        {
            if (AppConfig == null)
            {
                ConsoleUI.DisplayErrorMessage("The application settings file is either empty or " +
                    "could not be found! Terminating the application");
                Environment.Exit(0);
            }
            else
            {
                // Initialise the data connection
                if (!AppConfig.InTest)
                {
                    // We aren't in testing so set the spotifyDataClient to a live client
                    //_spotifyDataService = new LiveSpotifyDataService(AppConfig.AuthorisationUrl!, AppConfig.BaseURL!,
                    //    AppConfig.ClientID!, AppConfig.ClientSecret!);

                    if (!_spotifyDataService!.TestDataConnection())
                    {
                        // We don't have a successful connection to the spotify client
                        ConsoleUI.DisplayErrorMessage("Unable to connect to the spotify client! Terminating the application");
                        Environment.Exit(0);
                    }
                }
            }
        }

        /// <summary>
        /// The LoadAppSettings method attempts to load the app settings from file and initialise 
        /// the AppSettings property. If an exception is raised the method will initilaise the
        /// <see cref="AppSettings"/> property to a default InDevelopment/Test state.
        /// <param name="appSettingsFile">the name of the app config json file to read.</param>
        /// </summary>
        /// <returns>True if the app settings was sucessfully created from the file otherwise 
        /// false. </returns>
        public bool LoadAppSettings(string appSettingsFile)
        {
            // Load app configuration settings from the app settings file and set relevant feature flags.
            try
            {
                // Create a config object, using JSON provider specifying the appSetting.json file.
                IConfiguration appConfiguration = new ConfigurationBuilder()
                    .AddJsonFile(@"Raw\" + appSettingsFile)
                    .Build();

                // Get values from the appSettings.json configuation file
                AppConfig = appConfiguration.GetRequiredSection("AppConfig").Get<AppSettings>()!;
                ConsoleUI.DisplayMessage("Application settings file has been successfully loaded. Configuring the application.");
                return true;
            }
            catch (FileNotFoundException)
            {
                ConsoleUI.DisplayErrorMessage("The application settings file. AppSettings.json file was not " +
                    "found. Please ensure the AppSettings.json is included in the build");
            }
            return false;
        }

        /// <summary>
        /// The <c>MainMenuMaxOption</c> returns the MainMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int MainMenuMaxOption { get { return MainMenuMaxValue; } }

        /// <summary>
        /// The <c>SearchMenuMaxOption</c> returns the SearchMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int SearchMenuMaxOption { get { return SearchMenuMaxValue; } }

        /// <summary>
        /// The <c>ConfirmMenuMaxOption</c> returns the ConfirmMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int ConfirmMenuMaxOption { get { return ConfirmMenuMaxValue; } }

        /// <summary>
        /// The <c>OptionMenuMaxOption</c> returns the OptionMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int OptionMenuMaxOption { get { return OptionMenuMaxValue; } }

        /// <summary>
        /// The <c>FavouritesMenuMaxOption</c> returns the FavouritesMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int FavouritesMenuMaxOption { get { return FavouritesMenuMaxValue; } }

        /// <summary>
        /// The <c>RecFileMenuMaxOption</c> returns the RecFileMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int RecFileMenuMaxOption { get { return RecFileMenuMaxValue; } }

        /// <summary>
        /// The <c>RecMenuMaxOption</c> returns the RecMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int RecMenuMaxOption { get { return RecMenuMaxValue; } }

        /// <summary>
        /// The <c>RecMoodMenuMaxOption</c> returns the RecMoodMenuMaxValue to check that seleted option is
        /// within correct range
        /// </summary>
        public static int RecMoodMenuMaxOption { get { return RecMoodMenuMaxValue; } }

        /// <summary>
        /// The <c>Run</c> method displays the menu to the user.
        /// gets the users input and will continue to until user enters 0.
        /// uses a do while loop to impelenet this function
        /// </summary>
        public void Run()
        {

            ConsoleUI.LoadingGraphic();
            int userSelection;
            do
            {
                ConsoleUI.ClearConsole();
                ConsoleUI.DisplayMainMenu();

                userSelection = GetMenuOptionInput(MainMenuMaxOption);
                switch (userSelection)
                {
                    case 0:
                        break;
                    case 1:
                        ConsoleUI.ClearConsole();
                        GetNewAlbumReleases();
                        break;
                    case 2:
                        ConsoleUI.ClearConsole();
                        Recommendations();
                        break;
                    case 3:
                        ConsoleUI.ClearConsole();
                        Favourites();
                        break;
                    case 4:
                        ConsoleUI.ClearConsole();
                        StartSearch();
                        break;
                }
            } while (userSelection != 0);
        }


        /// <summary>
        /// The <c>StartSearch()</c> method prompts the user to search for
        /// an album, artist, or track, and passes the entered value into
        /// the <c>Search</c> method
        /// </summary>
        private void StartSearch()
        {

            int userSelection;
            string searchItemTypes = "";
            string filter;
            string searchQuery;

            ConsoleUI.ClearConsole();
            ConsoleUI.DisplaySearchMenu();
            userSelection = GetMenuOptionInput(SearchMenuMaxOption);
            ConsoleUI.ClearConsole();
            ConsoleUI.DisplayMessage("\nSearch\n");

            switch (userSelection)
            {
                case 1:
                    searchItemTypes = "album,artist";
                    ConsoleUI.DisplayMessage("Enter album name: ");
                    break;
                case 2:
                    searchItemTypes = "artist";
                    ConsoleUI.DisplayMessage("Enter artist name: ");
                    break;
                case 3:
                    searchItemTypes = "track,artist";
                    ConsoleUI.DisplayMessage("Enter track name: ");
                    break;
                case 0:
                    break;
            }

            if (userSelection == 0)
                return; //exit

            filter = searchItemTypes.Substring(searchItemTypes.IndexOf("=") + 1);
            searchQuery = GetSearchInput(filter, out string userTypedQuery);
            Search(searchQuery, searchItemTypes, userSelection, userTypedQuery);
        }


        /// <summary>
        /// The <c>Recommendations</c> method prompts the user to select
        /// an option which will get them recommendations based on either
        /// what they will search for passes to the <c>Search</c> method
        /// Their mood which passes user to <c>Search</c>
        /// Their favourites artists <c>Search</c>
        /// Their favourites Tracks <c>Search</c>
        /// </summary>
        private void Recommendations()
        {
            int recommendSelection;

            do
            {
                ConsoleUI.ClearConsole();
                ConsoleUI.DisplayRecommendationsMenu();
                recommendSelection = ConsoleUI.GetRecommendationsMenuOptionInput(RecMenuMaxOption);

                switch (recommendSelection)
                {
                    case 1:
                        StartSearch();
                        break;
                    case 2:
                        ConsoleUI.DisplayMoodsMenu();
                        RecMoods();
                        break;
                    case 3:
                        FavouriteArtistsRecomendations(favouriteArtists);
                        break;
                    case 4:
                        FavouriteTracksRecomendations(favouriteTracks);
                        break;
                    case 0:
                        break;
                }
            } while (recommendSelection != 0); // close do while loop for recommendations 

        }


        /// <summary>
        /// The <c>Search</c> method takes a search query and a search type
        /// as parameters to search the Spotify service and and returns the
        /// result of that search
        /// </summary>
        /// <param name="searchQuery"></param>
        /// <param name="searchItemTypes"></param>
        /// <param name="userSelection"></param>
        /// <param name="userTypedQuery">Captures what the user typed to re-show
        /// on the console</param>
        public void Search(string searchQuery, string searchItemTypes, int userSelection, string userTypedQuery)
        {
            SearchResults searchResults = _spotifyDataService!.Search(searchQuery, searchItemTypes);
            if (searchResults == null)
            {
                ConsoleUI.DisplayErrorMessage("Unable to complete search. Please try again.");
                return;
            }

            ConsoleUI.ClearConsole();
            ConsoleUI.DisplayMessage($"\nSearch results for '{userTypedQuery}':\n");

            switch (userSelection)
            {
                case 1:
                    List<Item> albums = searchResults!.Albums!.Items!;
                    ConsoleUI.DisplayAlbums(albums!);
                    break;
                case 2:
                    List<Item> artists = searchResults!.Artists!.Items!;
                    ConsoleUI.DisplayArtists(artists);
                    break;
                case 3:
                    List<Item> tracks = searchResults!.Tracks!.Items!;
                    ConsoleUI.DisplayTracks(tracks);
                    break;
            }

            ConsoleUI.LineBreak();
            ConfirmMenu(searchResults, userSelection); //passing through user selection as a way to
                                                       //capture what data it is (1=albums, 2=artists, 3=tracks)
        }

        /// <summary>
        /// The <c>Favourites</c> method prompts the user to select an option
        /// to view their favourite album, artist, or track,
        /// which is in the favourite Lists
        /// FavouriteArtists FavouriteTracks,FavouriteAlbums
        /// the user can add to the list by opening the <c>Search</c> method
        /// </summary>
        private void Favourites()
        {
            ConsoleUI.ClearConsole();
            int userSelection;
            do
            {
                ConsoleUI.DisplayFavouritesMenu();
                userSelection = GetMenuOptionInput(FavouritesMenuMaxOption);
                ConsoleUI.ClearConsole();
                switch (userSelection)
                {
                    case 1:
                        //If size of Lit is 0 then no data is in favourites
                        //Can not print favourites
                        if (favouriteAlbums.Count != 0)
                            ConsoleUI.DisplayFavouriteAlbums(favouriteAlbums!);
                        else
                        {
                            ConsoleUI.DisplayMessage("No Albums currently in Favourites");
                            Thread.Sleep(5);
                        }
                        break;
                    case 2:
                        if (favouriteArtists.Count != 0)
                            ConsoleUI.DisplayFavouriteArtists(favouriteArtists!);
                        else
                        {
                            ConsoleUI.DisplayMessage("No Artists currently in Favourites");
                            Thread.Sleep(5);
                        }
                        break;
                    case 3:
                        if (favouriteTracks.Count != 0)
                            ConsoleUI.DisplayFavouriteTracks(favouriteTracks!);
                        else
                        {
                            ConsoleUI.DisplayMessage("No Tracks currently in Favourites");
                            Thread.Sleep(5);
                        }
                        break;
                    case 4:
                        StartSearch();
                        break;
                    case 0:
                        break;
                }

            } while (userSelection != 0);
        }


        /// <summary>
        /// The <c>GetSearchInput</c> method vaidates the user input for search.
        /// </summary>
        /// <param name="filter">a string that specifies the type of data the user is searching for
        /// </param>
        /// <param name="userTypedQuery">Captures what the user typed and passes it back to
        /// be printed to console</param>
        /// <returns>A <c>string</c> representing the search query formatted to be passed to the
        /// spotify API</returns>
        public static string GetSearchInput(string filter, out string userTypedQuery)
        {
            //Create a string object called q
            string q;
            do
            {
                q = Console.ReadLine()!;
                userTypedQuery = q;
                if (q == "")
                {
                    ConsoleUI.DisplayMessage($"Invalid Entry. Please re-enter the name of the {filter}");
                }

            } while (q == "");
            q += "%20";//"%20" represents a space
            q = q.Replace(" ", "%20");
            //%3A represents a :
            return q.Insert(0, filter + "%3A");
        }

        /// <summary>
        /// The <c>GetMenuOptionInput</c> method vaidates the user input for a menu option.
        /// </summary>
        /// <param name="maxMenuOption"><c>int</c>an integer that specifies the maximum value for the menu.
        /// </param>
        /// <returns>An <c>int</c> representing the menu option selected by the user</returns>
        public static int GetMenuOptionInput(int maxMenuOption)
        {
            int validOption;
            while (true)
            {
                string? selection = ConsoleUI.ReadLine();
                if (int.TryParse(selection, out validOption) && validOption >= 0 && validOption <= maxMenuOption)
                    break;// break out of the while loop

                ConsoleUI.DisplayMessage($"\nInvalid entry. Please try again: ");
            }
            return validOption;
        }
        /// <summary>
        /// <c>ConfirmMenu</c> A menu which prompts the user to select which of a list of returned results is
        /// the correct one.
        /// </summary>
        /// <param name="searchResults"><c>searchResults</c> object containig
        /// the data returned from the search</param>
        /// <param name="userTypeSelection"><c>int</c> represents the type of data the user is searching for</param>
        public void ConfirmMenu(SearchResults searchResults, int userTypeSelection)
        {
            ConsoleUI.DisplayConfirmOptions(); //"confirm 1-5 OR return to search"
            int userResultSelection = GetMenuOptionInput(ConfirmMenuMaxOption);
            if (userResultSelection == 0)
                return;
            ConfirmResult(userResultSelection, searchResults, userTypeSelection);
        }

        /// <summary>
        /// <c>ConfirmResult</c> Get the id of the item the user confirmed, using the <paramref name="userResultSelection"/>
        /// as an index to get the correct item. Uses <paramref name="userTypeSelection"/>
        /// to capture what type of item it is.
        /// </summary>
        /// <param name="userResultSelection"><c>int</c>The index of the item the user confirmed.</param>
        /// <param name="searchedResults"><c>SearchResults</c>The list of search results.</param>
        /// <param name="userTypeSelection"><c>int</c>The type of item selected by the user.</param>
        private void ConfirmResult(int userResultSelection, SearchResults searchedResults, int userTypeSelection)
        {
            ConsoleUI.ClearConsole();
            Console.WriteLine("");
            //userResultSelection as the index of the desired object within its list
            string id = string.Empty;
            string name;
            switch (userTypeSelection)
            {
                // get the choosen item as an Item, extract the Item's ID,
                // use that ID to make an Album/Artist/Track object so
                // we can work with
                case 1:
                    List<Item> confirmAlbum = searchedResults.Albums?.Items!;
                    Item albumItem = confirmAlbum[userResultSelection - 1];
                    id = albumItem.Id!;

                    Album confirmedAlbum = GetAlbum(id);
                    name = confirmedAlbum.Name!;
                    ConsoleUI.DisplayMessage(name);
                    break;
                case 2:
                    List<Item> confirmArtist = searchedResults.Artists?.Items!;
                    Item artistItem = confirmArtist[userResultSelection - 1];
                    id = artistItem.Id!;

                    Artist confirmedArtist = GetArtist(id);
                    name = confirmedArtist.Name!;
                    ConsoleUI.DisplayMessage(name);
                    break;
                case 3:
                    List<Item> confirmTrack = searchedResults.Tracks?.Items!;
                    Item trackItem = confirmTrack[userResultSelection - 1];
                    id = trackItem.Id!;

                    Track confirmedTrack = GetTrack(id);
                    name = confirmedTrack.Name!;
                    ConsoleUI.DisplayMessage(name);
                    break;
            }

            Options(id, userTypeSelection); //could just pass in the object here instead of the ID of the object?
        }

        /// <summary>
        /// <c>Options</c> Handles whatever option the user chooses from the options menu
        /// </summary>
        /// <param name="id"><c>string</c> the id retrived from the search result</param>
        /// <param name="userTypeSelection"><c>int</c>The type of item selected by the user.</param>
        public void Options(string id, int userTypeSelection)
        {

            ConsoleUI.DisplayOptionsMenu();
            //"1. Add to favourites    2. Open link    3. Generate recommendations     4. Back    0. Main Menu"
            int userSelection = GetMenuOptionInput(OptionMenuMaxOption);

            switch (userSelection)
            {
                case 1:

                    AddFavourites(id, userTypeSelection);
                    //insert functionality
                    break;
                case 2:
                    OpenLink(id, userTypeSelection);
                    ConsoleUI.ClearConsole();
                    break; //where does this return to? might need a 'Press 1 to reshow, press 0 for main menu'
                case 3:
                    StartGenerateRecommendations(id, userTypeSelection);
                    break;
                case 4:
                    break;

            }
        }
        /// <summary>
        /// <c>StartGenerateRecommendations</c>
        /// begins generating the recommedations by getting the id of the albums, artists or tracks 
        /// </summary>
        /// <param name="id"><c>string</c> the id retrived from the search result</param>
        /// <param name="userTypeSelection"><c>int</c>The type of item selected by the user.</param>
        private void StartGenerateRecommendations(string id, int userTypeSelection)

        {

            string seedArtists = "";
            string seedGenres = "";
            string seedTracks = "";
            Recommendations recommendations = new();

            switch (userTypeSelection)
            {
                case 1: //tried using genres but albums don't seem to have any genres
                    Album album = GetAlbum(id);

                    StringBuilder albumTracks = new();
                    int counter = 0;
                    foreach (Track track in album.Tracks!.Tracks!)
                    {
                        if (counter >= 5)
                            break;

                        albumTracks.Append(track.Id);
                        albumTracks.Append(',');
                        counter++;
                    }

                    albumTracks.Remove(albumTracks.Length - 1, 1); //removing trailing comma
                    seedTracks = albumTracks.ToString();

                    recommendations = GetRecommendations(seedArtists, seedGenres, seedTracks, 10, "IE");

                    break;
                case 2: //genereate recs based on the chosen artist
                    seedArtists = id;
                    recommendations = GetRecommendations(seedArtists, seedGenres, seedTracks, 10, "IE");

                    break;
                case 3: //generate rec tracks based on the track chosen
                    seedTracks = id;
                    recommendations = GetRecommendations(seedArtists, seedGenres, seedTracks, 10, "IE");
                    break;
            }

            RecommendationsMenu(recommendations);

        }

        /// <summary>
        /// <c>RecommendationsMenu</c> Method that passes the genertaed recomendations to print them out
        /// Offers to user to recommendations to a file
        /// </summary>
        /// <param name="recommendations"><c>Recommendations</c> the genertaed recomendations </param>
        private static void RecommendationsMenu(Recommendations recommendations)
        {
            ConsoleUI.DisplayRecTracks(recommendations);
            ConsoleUI.LineBreak();
            ConsoleUI.DisplaySaveRecommendationsMenu();
            int userSelection = GetMenuOptionInput(RecFileMenuMaxOption);
            if (userSelection == 1)
            {
                WriteRecsToFile(recommendations);
                ConsoleUI.DisplayRecommendationsSuccess();
                ConsoleUI.ReadLine();
            }

        }

        /// <summary>
        /// <c>WriteRecsToFile</c> Method that writes the genertaed recomendations
        /// to a file called recommendations.txt
        /// </summary>
        /// <param name="recommendations"><c>Recommendations</c> the genertaed recomendations </param>
        static void WriteRecsToFile(Recommendations recommendations)
        {
            StringBuilder stringBuilder = new();
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            List<string> lines = new();
            foreach (Track tracks in recommendations.Tracks!)
            {
                stringBuilder.Append($"{tracks.Name} - ");
                bool firstArtist = true;
                foreach (Artist artist in tracks!.Artists!)
                {
                    if (!firstArtist)
                    {
                        stringBuilder.Append(", ");
                    }
                    stringBuilder.Append($"{artist.Name}\n");
                    firstArtist = false;
                }
                stringBuilder.Append($"https://https://open.spotify.com/track/{tracks.Id}");
                lines.Add(stringBuilder.ToString());
                stringBuilder.Clear();
            }
            using (StreamWriter outputFile = new(Path.Combine(docPath, "Recommendations.txt")))
            {
                foreach (string line in lines)
                    outputFile.WriteLine(line);
            }

        }



        /// <summary>
        /// <c>OpenLink</c> Opens a link in browser using the chosen object's ID
        /// </summary>
        /// <param name="id"><c>string</c> The id of the searched object</param>
        /// <param name="userTypeSelection"><c>int</c> represents the type of data user isn searching for</param>
        private static void OpenLink(string id, int userTypeSelection)
        {
            StringBuilder link = new();
            link.Append("https://open.spotify.com");
            if (userTypeSelection == 1)
                link.Append("/album/");
            if (userTypeSelection == 2)
                link.Append("/artist/");
            if (userTypeSelection == 3)
                link.Append("/track/");
            link.Append(id);

            try
            {
                ProcessStartInfo psi = new()
                {
                    FileName = link.ToString(),
                    UseShellExecute = true
                };
                Process.Start(psi);
            }
            catch (Exception exception)
            {
                Console.WriteLine($"An error occurred: {exception.Message}");
            }
        }
        /// <summary>
        /// <c>AddFavoutites</c> method takes in the parameters and chooses which list to add to
        /// </summary>
        /// <param name="id"><c>string</c> The id of the searched object</param>
        /// <param name="userTypeSelection"><c>int</c> represents the type of data user isn searching for</param>
        private void AddFavourites(string id, int userTypeSelection)
        {

            ConsoleUI.ClearConsole();
            switch (userTypeSelection)
            {
                case 1:
                    favouriteAlbums.Add(GetAlbum(id));
                    ConsoleUI.DisplayMessage("Adding " + GetAlbum(id).Name + " to favourites...");
                    break;
                case 2:
                    favouriteArtists.Add(GetArtist(id));
                    ConsoleUI.DisplayMessage("Adding " + GetArtist(id).Name + " to favourites...");
                    break;
                case 3:
                    favouriteTracks.Add(GetTrack(id));
                    ConsoleUI.DisplayMessage("Adding " + GetTrack(id).Name + " to favourites...");
                    break;
            }
            Thread.Sleep(5);
            ConsoleUI.DisplayMessage("Added To Favourites");
            Console.WriteLine("\nPress any key to continue");
            Console.ReadKey();
            ConsoleUI.ClearConsole();
        }


        /// <summary>
        /// The <c>GetNewAlbumReleases</c> method retrieves a list of new albums from the
        /// spotifyDataService and returns the list to the consoleUI to display. 
        /// </summary>
        private void GetNewAlbumReleases()
        {
            NewReleases newReleases = _spotifyDataService!.GetNewAlbumReleases();
            if (newReleases != null)
            {
                ConsoleUI.DisplayAlbumNewReleases(newReleases);
            }
            else
            {
                ConsoleUI.DisplayErrorMessage("Could not obtain a list of new album releases. Try again later");
            }
            Console.Write("Press any key to exit: ");
            Console.ReadKey();


        }

        /// <summary>
        /// The <c>GetTrack</c> method gets the Spotify catalog information
        /// for a single track identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id"><c>string</c> The track id</param>
        /// <returns>An <c>Track</c> representing the track that the id was given</returns>
        private Track GetTrack(string id)
        {
            Track track = _spotifyDataService!.GetTrack(id);
            return track!;
        }

        /// <summary>
        /// The <c>GetArtist</c> method gets Spotify catalog information for a
        /// single artist identified by their unique Spotify ID.
        /// </summary>
        /// <param name="id"><c>string</c> the Spotify IDs for the artist.</param>
        /// <returns>An <c>Artist</c> representing the track that the id was given</returns>
        private Artist GetArtist(string id)
        {
            Artist artist = _spotifyDataService!.GetArtist(id);
            return artist!;
        }

        /// <summary>
        /// The <c>GetAlbum</c> method gets the Spotify catalog information
        /// for a single album identified by its unique Spotify ID.
        /// </summary>
        /// <param name="id"><c>string</c> representing the album that the id was given</param>
        private Album GetAlbum(string id)
        {
            Album album = _spotifyDataService!.GetAlbum(id);
            return album!;
        }


        /// <summary>
        /// The <c>GetRecommendations</c> method is generated based on the
        /// available information for a given seed entity and matched against
        /// artists and tracks. If there is sufficient information about the
        /// provided seeds, a list of tracks will be returned together with pool
        /// size detail
        /// </summary>
        /// <param name="seedArtists"><c>string</c> artists Ids</param>
        /// <param name="seedGenres"><c>string</c> genres </param>
        /// <param name="seedTracks"><c>string</c> track ids</param>
        /// <param name="limit"><c>int</c> number of items to return</param>
        /// <param name="market"><c>string</c> what market is the data from</param>
        /// <returns>An <c>Recommendations</c> object </returns>
        private Recommendations GetRecommendations(string seedArtists, string seedGenres, string seedTracks, int limit = 20, string market = "IE")
        {
            Recommendations r = _spotifyDataService!.GetRecommendations(seedArtists, seedGenres, seedTracks, limit, market);
            if (r != null)
            {
                ConsoleUI.DisplayRecTracks(r);
            }

            else
            {
                ConsoleUI.DisplayErrorMessage("That's the recommendations");
            }
            return r!;
        }

        /// <summary>
        /// The <c>RecMoods</c> method gets the users current
        /// mood an passes it to the recommendations menu
        /// where it will br printed and displayed.
        /// /// </summary>
        private void RecMoods()
        {

            int userSelection;
            double max_danceability = 0;
            double max_energy = 0;
            double max_valence = 0;
            double max_liveness = 0;

            ConsoleUI.DisplayMoodsMenu();
            userSelection = GetMenuOptionInput(RecMoodMenuMaxOption);
            ConsoleUI.ClearConsole();
            do
            {
                switch (userSelection)
                {
                    case 1:
                        max_danceability = 1;
                        max_energy = 0.8;
                        max_valence = 1;
                        max_liveness = 1;
                        break;
                    case 2:
                        max_danceability = 0.25;
                        max_energy = 0.25;
                        max_valence = 0.45;
                        max_liveness = 0.2;
                        break;
                    case 3:
                        max_danceability = 0.8;
                        max_energy = 0.8;
                        max_valence = 0.3;
                        max_liveness = 0.8;
                        break;
                    case 4:
                        max_danceability = 0.05;
                        max_energy = 0.25;
                        max_valence = 0.5;
                        max_liveness = 0.1;
                        break;
                    case 0:
                        break;
                }

                RecommendationsMenu(GetMoodRecommendations(5, max_danceability, max_energy, max_valence, max_liveness));
                ConsoleUI.DisplayMoodsMenu();
                userSelection = GetMenuOptionInput(RecMoodMenuMaxOption);
                ConsoleUI.ClearConsole();
            } while (userSelection != 0);

        }

        /// <summary>
        /// The <c>FavouriteArtistsRecomendations</c> method gets the users 
        /// favourite artists an passes it to the recommendations menu
        /// where it will br printed and displayed.
        /// </summary>
        /// <param name="artists"><c>List Artist</c>list of favourite artist</param>
        private void FavouriteArtistsRecomendations(List<Artist> artists)
        {

            ConsoleUI.ClearConsole();
            StringBuilder stringBuilder = new();
            int i = 0;
            foreach (Artist artist in artists)
            {
                i++;
                if (i == 5)
                    break;
                stringBuilder.Append($"{artist?.Id},");
            }

            StartGenerateRecommendations(stringBuilder.ToString(), 2);
            ConsoleUI.ClearConsole();
        }

        /// <summary>
        /// The <c>FavouriteTracksRecomendations</c> method gets the users 
        /// favourite tracks an passes it to the recommendations menu
        /// where it will br printed and displayed.
        /// </summary>
        /// <param name="tracks"><c>List Track</c>list of favourite tracks</param>
        private void FavouriteTracksRecomendations(List<Track> tracks)
        {

            ConsoleUI.ClearConsole();
            StringBuilder stringBuilder = new();
            int i = 0;
            foreach (Track track in tracks)
            {
                i++;
                if (i == 5)
                    break;
                stringBuilder.Append($"{track?.Id},");
            }

            StartGenerateRecommendations(stringBuilder.ToString(), 3);
            ConsoleUI.ClearConsole();
        }

        /// <summary>
        /// The <c>GetMoodRecommendations</c> method is used to get return
        /// recommendations based on what the users current mood
        /// </summary>
        /// <param name="limit"><c>int</c> limit to the number of records returned</param>
        /// <param name="max_danceability"><c>double</c>value for danceability</param>
        /// <param name="max_energy"><c>double</c>value for energy</param>
        /// <param name="max_valence"><c>double</c>value for valence</param>
        /// <param name="max_liveness"><c>double</c>value for liveness</param>
        private Recommendations GetMoodRecommendations(int limit, double max_danceability, double max_energy, double max_valence, double max_liveness)
        {
            Recommendations r = _spotifyDataService!.GetMoodRecommendations(limit, max_danceability, max_energy, max_valence, max_liveness);
            return r!;
        }


    }

}


