using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DeezerDownloader.Core.Models;

namespace DeezerDownloader.Core
{
    public class Downloader
    {
        private AudioDownloader AudioDownloader { get; }
        private DeezerClient Deezer { get; set; }
        public delegate void ProgressEventHandler(double p, Track track);
        public event ProgressEventHandler ProgressChangedEvent;

        public Downloader()
        {
            AudioDownloader = new AudioDownloader();
            Deezer = new DeezerClient();
            ProgressChangedEvent += OnProgressChangedEvent;
        }

        public async Task DownloadDeezerUrlOfType(
            string rootPath, 
            long id, 
            string linkType,
            CancellationToken cancellationToken = default
        )
        {
            switch (linkType)
            {
                case "profile":
                    await DownloadUserPlaylists(rootPath, id, cancellationToken);
                    await DownloadFavouriteTracks(rootPath, id, cancellationToken);
                    break;
                case "loved":
                    await DownloadFavouriteTracks(rootPath, id, cancellationToken);
                    break;
                case "album":
                    await DownloadAlbum(rootPath, id, cancellationToken);
                    break;
                 case "track":
                     Track track = Deezer.GetTrackById(id);
                     await DownloadTrack(track, Path.Combine(rootPath, track.Title+".mp3"), cancellationToken);
                     break;
                case "playlist":
                    await DownloadPlaylist(rootPath, cancellationToken, id);
                    break;
            }
        }

        public async Task DownloadUserPlaylists(string rootPath, long userId, CancellationToken cancellationToken = default)
        {
            List<Playlist> playlists = Deezer.GetPlaylistsByUserId(userId);
            if (playlists.Count == 0)
                return;

            foreach (Playlist playlist in playlists)
            {
                await DownloadPlaylist(rootPath, cancellationToken, playlist.Id);
            }
        }

        public async Task DownloadFavouriteTracks(string rootPath, long userId,
            CancellationToken cancellationToken = default)
        {
            List<Track> tracks = Deezer.GetFavouriteTracksByUserId(userId);
            Creator creator = Deezer.GetCreatorByid(userId);

            Console.WriteLine($"Starting Download for Favourite Songs of: {creator.Name}");
            await DownloadTracks(tracks, Path.Combine(rootPath, creator.Name, "Lieblingssongs"), cancellationToken);
            Console.WriteLine($"Download for Favourite Songs of {creator.Name} has been successful!");
        }

        public async Task DownloadTracks(List<Track> tracks, string tracksPath,
            CancellationToken cancellationToken = default)
        {
            foreach (Track track in tracks)
            {
                await DownloadTrack(track, Path.Combine(tracksPath, $"{track.Artist.Name} - {track.Title}.mp3"), cancellationToken);
            }
        }

        public async Task DownloadPlaylist(string rootPath, CancellationToken cancellationToken = default, long playlistId = 0, Playlist playlist = null)
        {
            if (playlistId == 0 && playlist == null)
                throw new Exception();

            if (playlist != null && playlist.Tracks == null)
                playlistId = playlist.Id;

            if (playlistId != 0)
                playlist = Deezer.GetPlaylistById(playlistId);

            List<Track> tracks = playlist.Tracks.Data;
            
            Console.WriteLine($"Starting Download for Playlist: {playlist.Title}");
            await DownloadTracks(tracks, Path.Combine(rootPath, playlist.Creator.Name, playlist.Title), cancellationToken);
            Console.WriteLine($"Download of Playlist {playlist.Title} has been successful!");
        }

        public async Task DownloadAlbum(string rootPath, long albumId, CancellationToken cancellationToken = default)
        {
            Album album = Deezer.GetAlbumById(albumId);
            
            Console.WriteLine($"Starting Download for Album: {album.Title}");
            foreach (Track track in album.Tracks.Data)
            {
                await DownloadTrack(track, Path.Combine(rootPath, album.Title, track.Title + ".mp3"), cancellationToken);
            }
            
            Console.WriteLine($"Download of Album {album.Title} has been successful!");
        }

        public async Task DownloadTrack(Track track, string filePath, CancellationToken cancellationToken = default)
        {
            if (File.Exists(filePath))
                return;

            Progress<double> progress = new Progress<double>(p => ProgressChangedEvent.Invoke(p, track));
            await AudioDownloader.DownloadAudioAsnyc(track, filePath, progress, cancellationToken);
        }
        
        private void OnProgressChangedEvent(double progress, Track track)
        {
            int totalBlockCount = 36;
            int blockCount = progress.Map(0, 1, 1, totalBlockCount + 1);
            blockCount = totalBlockCount < blockCount ? totalBlockCount : blockCount;
            string newLine = (int)Math.Round(progress * 100) == 100 ? "\n" : "";
            string text = $"\rDownloading... {track.Artist.Name} - {track.Title} Progress: " +
                          $"[{new string('=', blockCount)}>{new string('-', totalBlockCount - blockCount)}] " +
                          $"{Math.Round(progress * 100, 1)} %  {newLine}";
            
            Console.Write(text);
        }
    }
}