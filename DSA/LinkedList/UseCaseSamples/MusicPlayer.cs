namespace DSA.LinkedList.UseCaseSamples;

using System;
using System.Collections.Generic;

public class Song
{
    public string Title { get; }
    public string Artist { get; }
    public int DurationInSeconds { get; }

    public Song(string title, string artist, int durationInSeconds)
    {
        Title = title;
        Artist = artist;
        DurationInSeconds = durationInSeconds;
    }
}

public class Playlist
{
    private LinkedList<Song> songs = new LinkedList<Song>();

    public void AddSong(Song song)
    {
        songs.AddLast(song);
    }

    public void RemoveSong(Song song)
    {
        songs.Remove(song);
    }

    public void PrintPlaylist()
    {
        foreach (var song in songs)
        {
            Console.WriteLine($"{song.Title} - {song.Artist} ({song.DurationInSeconds} seconds)");
        }
    }
}

public class MusicPlayer
{
    private Playlist currentPlaylist;

    public void CreatePlaylist()
    {
        currentPlaylist = new Playlist();
    }

    public void AddSongToPlaylist(Song song)
    {
        if (currentPlaylist != null)
        {
            currentPlaylist.AddSong(song);
        }
        else
        {
            Console.WriteLine("No playlist created. Please create a playlist first.");
        }
    }

    public void RemoveSongFromPlaylist(Song song)
    {
        if (currentPlaylist != null)
        {
            currentPlaylist.RemoveSong(song);
        }
        else
        {
            Console.WriteLine("No playlist created. Please create a playlist first.");
        }
    }

    public void PlayCurrentPlaylist()
    {
        if (currentPlaylist != null)
        {
            Console.WriteLine("Playing the current playlist:");
            currentPlaylist.PrintPlaylist();
        }
        else
        {
            Console.WriteLine("No playlist created. Please create a playlist first.");
        }
    }
}

public static class ProgramMusicPlayer
{
    public static void Apply()
    {
        MusicPlayer musicPlayer = new MusicPlayer();

        // Create a playlist
        musicPlayer.CreatePlaylist();

        // Add songs to the playlist
        Song song1 = new Song("Song 1", "Artist 1", 180);
        Song song2 = new Song("Song 2", "Artist 2", 220);
        musicPlayer.AddSongToPlaylist(song1);
        musicPlayer.AddSongToPlaylist(song2);

        // Play the current playlist
        musicPlayer.PlayCurrentPlaylist();

        // Remove a song from the playlist
        musicPlayer.RemoveSongFromPlaylist(song1);

        // Play the updated playlist
        musicPlayer.PlayCurrentPlaylist();
    }
}
