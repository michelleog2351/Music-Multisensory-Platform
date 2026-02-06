namespace MusicBased_IOT_Platform
{
    /* 
   * Filename: ConsoleUI.cs
   * Description: Contains the definition of the consoleUI class. 
   */

    using MusicBased_IOT_Platform.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;


    /// <summary>
    /// The ConsoleUI class is used to provide a console user interface. The console class
    /// provides primary functions for displaying data and validating user input. There should
    /// be minimal logic within the ConsoleUI class. 
    /// </summary>
    public static class ConsoleUI
    {
        // Fields


        // Static constructor for the ConsoleUI class
        //static ConsoleUI()
        //{
        //    // Set the console to UTF16
        //    Console.OutputEncoding = Encoding.Unicode;
        //}

        //// Methods


        ///// <summary>
        ///// Simple graphic as a splash screen
        ///// </summary>
        //public static void LoadingGraphic()
        //{
        //    List<string> graphicLines = new();
        //    ClearConsole();
        //    graphicLines.Add("                 üüüüüüüüüüüüü");
        //    graphicLines.Add("            aüüüüüüüüüüüüüüüüüüüüü&");
        //    graphicLines.Add("         ôüüüüüüüüüüüüüüüüüüüüüüüüüüüa");
        //    graphicLines.Add("       üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("      üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("    üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("   üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("  üüüüüüa                       aüüüüüüüüüüüü");
        //    graphicLines.Add(" üüüüüü                              aüüüüüüüü");
        //    graphicLines.Add(" üüüüüüü   üüüüüüüüüüüüüüüüüüü          üüüüüü");
        //    graphicLines.Add("üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü     aüüüüüü");
        //    graphicLines.Add("üüüüüüüüüüü                 üüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("üüüüüüüüü                         üüüüüüüüüüüüü");
        //    graphicLines.Add("üüüüüüüüüü üüüüüüüüüüüüüüüüü         üüüüüüüüüü");
        //    graphicLines.Add("üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü    üüüüüüüüüü");
        //    graphicLines.Add(" üüüüüüüüüüü               aüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add(" üüüüüüüüü                      üüüüüüüüüüüüüü");
        //    graphicLines.Add("  üüüüüüüüüüüüüüüüüüüüüüüüüüü     üüüüüüüüüüü");
        //    graphicLines.Add("   üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("    üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("      üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("       üüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüüü");
        //    graphicLines.Add("         aüüüüüüüüüüüüüüüüüüüüüüüüüüüa");
        //    graphicLines.Add("            &üüüüüüüüüüüüüüüüüüüüüa");
        //    graphicLines.Add("                 üüüüüüüüüüüüü");

        //    foreach (string line in graphicLines)
        //    {
        //        Console.ForegroundColor = ConsoleColor.DarkGreen;
        //        Console.WriteLine(line);
        //        Thread.Sleep(10);
        //    }
        //    Console.ForegroundColor = ConsoleColor.White;
        //    Console.WriteLine("\n");
        //    Console.WriteLine("              Welcome to Spotify!");
        //    Console.WriteLine("           Press any key to enter...");
        //    Console.ReadKey();
        //}

        ///// <summary>
        ///// A method to display the main menu options on the console.
        ///// </summary>
        //public static void DisplayMainMenu()
        //{
        //    List<string> options = new()
        //    {
        //        "\nMain Menu\n",
        //        "1. See newest album releases",
        //        "2. Recommendations",
        //        "3. Favourites",
        //        "4. Search",
        //        "0. Exit the application"
        //    };
        //    foreach (string option in options)
        //    {
        //        Console.WriteLine(option);
        //        Thread.Sleep(5);
        //    }

        //    Console.Write("\nPlease select an option: ");
        //}

        ///// <summary>
        ///// A method to display a sub menu options to the console for the search functionality.
        ///// Different menu options 
        ///// </summary>
        //public static void DisplaySearchMenu()
        //{
        //    ClearConsole();
        //    List<string> options = new()
        //    {
        //        "\nSearch\n",
        //        "1. Search for Album",
        //        "2. Search for Artist",
        //        "3. Search for Track",
        //        "0. Exit Search Menu"
        //    };
        //    foreach (string option in options)
        //    {
        //        Console.WriteLine(option);
        //        Thread.Sleep(5);
        //    }
        //    Console.Write("\nPlease select an option: ");
        //}


        ///// <summary>
        ///// A method to display a sub menu options to the console for the Favourites functionality.
        ///// Different menu options 
        ///// </summary>
        //public static void DisplayFavouritesMenu()
        //{
        //    DisplayMessage("\n FAVOURITES \n");
        //    List<string> options = new()
        //    {
        //        "1. Album",
        //        "2. Artist",
        //        "3. Track",
        //        "",
        //        "4. Add More To Favourites",
        //        "0. Exit Search Menu"
        //    };
        //    foreach (string option in options)
        //    {
        //        Console.WriteLine(option);
        //        Thread.Sleep(5);
        //    }
        //    Console.Write("\nPlease select an option: ");
        //}


        ///// <summary>
        ///// Displays a prompt to enter the number corresponding to the correct
        ///// search result, or 0 to return to search
        ///// </summary>
        //public static void DisplayConfirmOptions()
        //{
        //    Console.WriteLine("Enter a number to select a result, or enter 0 to return to search: ");
        //}

        ///// <summary>
        ///// Displays a menu giving the user 4 options to choose from
        ///// </summary>
        //public static void DisplayOptionsMenu()
        //{
        //    Console.WriteLine("\n1. Add to favourites    2. Open link    3. Generate Recommendations    4. Back    0. Main Menu");
        //}
        ///// <summary>
        ///// Displays a menu after recommendations asking if the user
        ///// wants to save the recommendations to file
        ///// </summary>
        //public static void DisplaySaveRecommendationsMenu()
        //{
        //    Console.WriteLine("Save recommendations to file?\n1. OK    0. Exit");
        //}

        ///// <summary>
        ///// A method to display the Recommended Menu options to the console.
        ///// Here the user has options to select from recommended artists, recommended tracks
        ///// or return back to the main menu. We could hardcode the menu
        ///// options for each menu or we could load them from a text file and create menu options
        ///// dynamically. 
        ///// </summary>
        //public static void DisplayRecommendationsMenu()
        //{
        //    ClearConsole();
        //    List<string> options = new()
        //    {

        //        "\nRECOMMENDATIONS",

        //        "1. Get Recomendations on something you are looking for",

        //        "2. Recommendations based on mood",

        //        "3. Recommendations based on Favourite Artists",

        //        "4. Recommendations based on Favourite Tracks",

        //        "0. Return back to Main Menu"
        //    };

        //    foreach (string option in options)
        //    {
        //        Console.WriteLine(option);
        //        Thread.Sleep(5);
        //    }
        //    Console.Write("\nPlease select from one of the available options: ");
        //}

        ///// <summary>
        ///// The <c>GetMenuOptionInput</c> method vaidates the user input for a menu option.
        ///// </summary>
        ///// <param name="maxMenuOption">an integer that specifies the maximum value for the menu.
        ///// </param>
        ///// <returns>An <c>int</c> representing the menu option selected by the user</returns>
        //public static int GetRecommendationsMenuOptionInput(int maxMenuOption)
        //{
        //    int validOption;
        //    while (true)
        //    {
        //        string? selection = Console.ReadLine();
        //        if (int.TryParse(selection, out validOption) && validOption >= 0 && validOption <= maxMenuOption)
        //            break;

        //        // NB!! Clear the screen function
        //        // Might want to clear the screen and redisplay the menu being displayed
        //        Console.Write($"\nInvalid entry. Please try again: ");
        //    }
        //    return validOption;
        //}


        ///// <summary>
        ///// A method to display the Recommended Moods Menu options to the console.
        ///// Here the user has options to select from which mood they are feeling
        ///// or return back to the main menu.
        ///// </summary>
        //public static void DisplayMoodsMenu()
        //{
        //    ClearConsole();
        //    List<string> options = new List<string>()
        //    {
        //        "\nHey! How are you feeling today?",

        //        "1. Happy/Joyful",

        //        "2. Feeling a bit down in the dumps?",

        //        "3. ABSOLUTELY INFURIATED",

        //        "4. How about some calm, relaxing music?",

        //        "0. Return back to Main Menu",
        //    };

        //    foreach (string option in options)
        //    {
        //        Console.WriteLine(option);
        //        Thread.Sleep(5);
        //    }
        //    Console.Write("\nPlease select from one of the available options: ");
        //}


        ///// <summary>
        ///// The <c>DisplayArtist</c> method takes the Artist object and writes the name
        ///// out to the console
        ///// </summary>
        ///// <param name="artists">An instance of <c>Artist</c> containing the serialised
        ///// response from the Spotify API detailing artist informaion.</param>
        //public static void DisplayFavouriteArtists(List<Artist> artists)
        //{
        //    DisplayMessage("\n FAVOURITE ARTISTS \n");
        //    StringBuilder stringBuilder = new();
        //    foreach (Artist artist in artists)
        //    {
        //        stringBuilder.Append($"{artist.Name}\n");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //    }

        //}

        /// <summary>
        /// The <c>DisplayArtists</c> method takes a list of Items containing artist info
        /// and writes the Artist names out to the console
        /// </summary>
        /// <param name="artists">An instance of <c>ArtistsList</c> containing the serialised
        /// response from the Spotify API detailing artists informaion.</param>
        public static void DisplayArtists(List<Item> artists)
        {
            int i = 1;

            StringBuilder stringBuilder = new();
            foreach (Item artist in artists)
            {
                stringBuilder.Append($"{i}. {artist.Name}");
                DisplayMessage(stringBuilder.ToString());
                Thread.Sleep(5);
                stringBuilder.Clear();
                i++;
            }
        }

        ///// <summary>
        ///// Appends artist names to albums and tracks.
        ///// </summary>
        ///// <param name="artists"></param>
        ///// <param name="stringBuilder"></param>
        //public static void AppendArtistsNames(List<Artist> artists, StringBuilder stringBuilder)
        //{
        //    if (artists == null)
        //        return;
        //    bool firstArtist = true;
        //    foreach (Artist artist in artists)
        //    {
        //        if (!firstArtist)
        //        {
        //            stringBuilder.Append(", ");
        //        }
        //        stringBuilder.Append($"{artist.Name}");
        //        firstArtist = false;
        //    }
        //}

        ///// <summary>
        ///// The <c>DisplayTrack</c> method takes a given track and prints it onto
        ///// the console</summary>
        ///// <param name="tracks">An instance of <c>Track</c> containing the serialised
        ///// response from the Spotify API detailing the track to be returned.</param>
        //public static void DisplayFavouriteTracks(List<Track> tracks)
        //{
        //    DisplayMessage("\nFAVOURITE TRACKS \n");

        //    StringBuilder stringBuilder = new();
        //    foreach (Track track in tracks)
        //    {
        //        stringBuilder.Append($"{track?.Name} - ");
        //        AppendArtistsNames(track!.Artists!, stringBuilder);
        //        DisplayMessage(stringBuilder.ToString());
        //        stringBuilder.Clear();
        //    }
        //}

        ///// <summary>
        ///// The <c>DisplayTrack</c> method takes a given track and prints it onto
        ///// the console</summary>
        ///// <param name="track">An instance of <c>Track</c> containing the serialised
        ///// response from the Spotify API detailing the track to be returned.</param>
        //public static void DisplayTrack(Track track)
        //{
        //    DisplayMessage("\nTRACK \n");

        //    StringBuilder stringBuilder = new();
        //    stringBuilder.Append($"Track Name {track.Name}");
        //    DisplayMessage(stringBuilder.ToString());
        //    Thread.Sleep(5);
        //    stringBuilder.Clear();
        //}

        ///// <summary>
        ///// The <c>DisplayTracks</c> method takes the given list of tracks
        ///// and prints them on the console</summary>
        ///// <param name="tracks">A list of Tracks</param>
        //public static void DisplayTracks(List<Item> tracks)
        //{
        //    int i = 1;
        //    StringBuilder stringBuilder = new();
        //    foreach (Item track in tracks)
        //    {
        //        stringBuilder.Append($"{i}. {track?.Name} - ");
        //        AppendArtistsNames(track!.Artists!, stringBuilder);
        //        stringBuilder.Append($" ({track.ReleaseDate})");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //        i++;
        //    }

        //}

        ///// <summary>
        ///// The <c>DisplayRecTracks</c> method takes the given recommended
        ///// tracks and prints them onto the console</summary>
        ///// <param name="recommended">An instance of <c>Recommendations</c>
        ///// containing the serialised response from the Spotify API detailing
        ///// the recommended tracks</param>
        //public static void DisplayRecTracks(Recommendations recommended)
        //{
        //    ClearConsole();
        //    DisplayMessage("\nRecommended Tracks \n");

        //    StringBuilder stringBuilder = new();
        //    foreach (Track tracks in recommended.Tracks!)
        //    {
        //        stringBuilder.Append($"{tracks.Name} - ");
        //        AppendArtistsNames(tracks!.Artists!, stringBuilder);
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //    }
        //}


        ///// <summary>
        ///// The <c>DisplayRecommendationsSuccess</c> method used to
        ///// tell user that the recommendations have been saved to file</summary>
        //public static void DisplayRecommendationsSuccess()
        //{
        //    ClearConsole();
        //    string dot = ".";
        //    for (int i = 0; i < 3; i++)
        //    {
        //        Console.Write(dot);
        //        Thread.Sleep(500);
        //    }
        //    DisplayMessage("\n\nYour recommendations have been saved. You can find them in your Documents folder.");
        //    DisplayMessage("Press any key to exit.");
        //}

        ///// <summary>
        ///// The <c>DisplayAlbum</c> method takes the given album and prints them onto
        ///// the console, taking care of out formatting.</summary>
        ///// <param name="albums">An instance of <c>Album</c> containing the serialised
        ///// response from the Spotify API detailing the album to be returned.</param>
        //public static void DisplayFavouriteAlbums(List<Album> albums)
        //{

        //    DisplayMessage("\nFAVOURITE ALBUMS \n");
        //    StringBuilder stringBuilder = new();
        //    foreach (Album album in albums)
        //    {
        //        stringBuilder.Append($"{album?.Name} - ");
        //        AppendArtistsNames(album!.Artists!, stringBuilder);
        //        stringBuilder.Append($" ({album.ReleaseDate!.AsSpan(0, 4)})");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //    }
        //}

        ///// <summary>
        ///// The <c>DisplayAlbums</c> method takes the given albums and prints them onto
        ///// the console, taking care of out formatting.</summary>
        ///// <param name="albums">A list of items containing the serialised
        ///// response from the Spotify API detailing the albums to be returned.</param>
        //public static void DisplayAlbums(List<Item> albums)
        //{
        //    int i = 1;
        //    StringBuilder stringBuilder = new();
        //    foreach (Item album in albums)
        //    {
        //        stringBuilder.Append($"{i}. {album?.Name} - ");
        //        AppendArtistsNames(album!.Artists!, stringBuilder);
        //        stringBuilder.Append($" ({album.ReleaseDate!.AsSpan(0, 4)})");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //        i++;
        //    }
        //}

        ///// <summary>
        ///// The <c>DislpayArtistAlbum</c> method takes the given albums from <c>ArtistAlbums</c>
        ///// the and prints them onto the console, taking care of out formatting.
        ///// </summary>
        ///// <param name="albums">An instance of <c>ArtistAlbums</c> containing the serialised
        ///// response from the Spotify API detailing the tracks to be returned.</param>
        //public static void DisplayArtistAlbum(ArtistAlbums albums)
        //{
        //    DisplayMessage("\nArtists Top Tracks \n");

        //    StringBuilder stringBuilder = new();
        //    foreach (Album album in albums.Items!)
        //    {
        //        stringBuilder.Append($"Album Name {album.Name} \n ");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //    }
        //}

        ///// <summary>
        ///// The <c>DisplayArtistTopTracks</c> method takes the given track from <c>ArtistTopTracks</c>
        ///// the and prints them onto the console, taking care of out formatting the output
        ///// </summary>
        ///// <param name="tracks">An instance of <c>ArtistTopTracks</c> containing the serialised
        ///// response from the Spotify API detailing the tracks to be returned.</param>
        //public static void DisplayArtistTopTracks(ArtistTopTracks tracks)
        //{
        //    DisplayMessage("\nTop Tracks \n");

        //    StringBuilder stringBuilder = new();
        //    foreach (Track topTrack in tracks.TopTracks!)
        //    {
        //        stringBuilder.Append($"Track Name {topTrack.Name} \n ");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //    }
        //}


        ///// <summary>
        ///// The <c>DisplayAlbumNewReleases</c> method takes the new releases and writes the content
        ///// out to the console, taking care of out formatting.
        ///// <param name="releases">An instance of <c>NewReleases</c> containing the serialised
        ///// response from the Spotify API detailing new releases.</param>
        ///// </summary>

        //public static void DisplayAlbumNewReleases(NewReleases releases)
        //{
        //    DisplayMessage("\nNew album releases \n");

        //    StringBuilder stringBuilder = new();
        //    foreach (Item release in releases.Albums!.Items!)
        //    {
        //        stringBuilder.Append($"{release.Name} - ");
        //        AppendArtistsNames(release.Artists!, stringBuilder);
        //        stringBuilder.Append($"{release.ReleaseDate!.AsSpan(0, 4)}");
        //        DisplayMessage(stringBuilder.ToString());
        //        Thread.Sleep(5);
        //        stringBuilder.Clear();
        //    }
        //    Thread.Sleep(500);
        //    //New line to separate main menu and the new Releases data when printed
        //    DisplayMessage("\n");
        //}



        ///// <summary>
        ///// The <c>DisplayErrorMessage</c> method writes the string provided out as an error
        ///// message.
        ///// </summary>
        ///// <param name="errorMesssage">The <c>string</c> error message to write to the console.
        ///// </param>
        //public static void DisplayErrorMessage(string errorMesssage)
        //{
        //    Console.WriteLine($"\nAn error has occurred: {errorMesssage}\n");
        //}

        /// <summary>
        /// The <c>DisplayMessage</c> method writes the string provided out as an error
        /// message.
        /// </summary>
        /// <param name="message">The <c>string</c> message to write to the console.
        /// </param>
        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);

        }

        ///// <summary>
        ///// <c>ClearConsole</c> clears the console
        ///// </summary>
        //public static void ClearConsole()
        //{
        //    Console.Clear();
        //}

        ///// <summary>
        ///// Returns user input
        ///// </summary>
        //public static string ReadLine()
        //{
        //    return Console.ReadLine()!;
        //}
        ///// <summary>
        ///// Prints a line break to the console
        ///// </summary>
        //public static void LineBreak()
        //{
        //    Console.WriteLine("\n");
        //}

    }
}
