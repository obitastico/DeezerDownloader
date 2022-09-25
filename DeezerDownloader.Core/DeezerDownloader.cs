using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeezerDownloader.Core.Models;
using DeezerDownloader.Core.Tagging;
using Gress;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace DeezerDownloader.Core
{
    public class DeezerDownloader
    {
        private AudioDownloader AudioDownloader { get; }
        private MediaTagInjector MediaTagInjector { get; set; }
        private DeezerClient Deezer { get; set; }

        public DeezerDownloader()
        {
            AudioDownloader = new AudioDownloader();
            Deezer = new DeezerClient();
            MediaTagInjector = new MediaTagInjector();
        }

        public async Task DownloadUserPlaylists(long userId, string rootPath)
        {
            List<Playlist> playlists = Deezer.GetPlaylistsByUserId(userId);
            if (playlists.Count == 0)
                return;

            foreach (Playlist playlist in playlists)
            {
                await DownloadPlaylist(Path.Combine(rootPath, playlist.Creator.Name, playlist.Title), playlist.Id);
                break; //// TODO: for final
            }
        }

        public async Task DownloadPlaylist(string folderPath, long playlistId = 0, Playlist playlist = null)
        {
            if (playlistId == 0 && playlist == null)
                throw new Exception();

            if (playlist != null && playlist.Tracks == null)
                playlistId = playlist.Id;

            if (playlistId != 0)
                playlist = Deezer.GetPlaylistById(playlistId);

            List<Track> tracks = playlist.Tracks.Data;
            
            Console.WriteLine($"Starting Download for: {playlist.Title}");
            foreach (Track track in tracks)
            {
                await DownloadTrack(track, Path.Combine(folderPath, track.Title+".mp3"));
                break; //// TODO: for final
            }
            
            Console.WriteLine($"Download of {playlist.Title} has been successful!");
        }

        public async Task DownloadTrack(Track track, string filePath)
        {
            // try
            // {
            //     File.Delete(filePath);
            // } catch (DirectoryNotFoundException) {}
            //// TODO: Undo comment
            
            Progress<double> progress = new Progress<double>(p => ProgressHandler(p, track));
            await AudioDownloader.DownloadAudioAsnyc(track, filePath, progress);
        }

        private void ProgressHandler(double progress, Track track)
        {
            int totalBlockCount = 36;
            int blockCount = progress.Map(0, 1, 0, totalBlockCount + 1);
            string newLine = (int)Math.Round(progress * 100) == 100 ? "\n" : "";
            string text = $"\rDownloading... {track.Artist.Name} - {track.Title} Progress: " +
                          $"[{new string('=', blockCount)}>{new string('-', totalBlockCount - blockCount)}] " +
                          $"{Math.Round(progress * 100, 1)} %  {newLine}";
            
            Console.Write(text);
        }
    }
}