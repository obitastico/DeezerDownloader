using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeezerDownloader.Core.Models;
using Gress;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;
using YoutubeExplode.Videos.Streams;

namespace DeezerDownloader.Core
{
    public class DeezerDownloader
    {
        private YoutubeClient Youtube { get; set; }
        private DeezerClient Deezer { get; set; }

        public DeezerDownloader()
        {
            Youtube = new YoutubeClient();
            Deezer = new DeezerClient();
        }

        public async Task DownloadPlaylist(long playlistId = 0, Playlist playlist = null)
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
                await DownloadTrack(track, $@"{AppDomain.CurrentDomain.BaseDirectory}out\{playlist.Title}\{track.Title}.mp3");
            }
            Console.WriteLine($"Download of {playlist.Title} has been successful!");
        }

        public async Task DownloadTrack(Track track, string filePath)
        {
            try
            {
                File.Delete(filePath);
            } catch (DirectoryNotFoundException) {}
            
            string sQuery = $"{track.Artist.Name} - {track.Title}";
            VideoSearchResult videoInfo = (await Youtube.Search.GetVideosAsync(sQuery).CollectAsync(1)).FirstOrDefault();

            if (videoInfo == null)
                throw new Exception("Cannot get Video from Youtube");
            
            StreamManifest streamManifest = await Youtube.Videos.Streams.GetManifestAsync(videoInfo.Url);
            IStreamInfo streamInfo = streamManifest.GetAudioOnlyStreams().GetWithHighestBitrate();
            
            var dirPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dirPath))
                Directory.CreateDirectory(dirPath);

            double lastProgress = 0;
            Progress<Percentage> progress = new Progress<Percentage>(p => ProgressHandler(p, track, ref lastProgress));
            await Youtube.Videos.Streams.DownloadAsync(streamInfo, filePath, progress.ToDoubleBased());
        }

        private void ProgressHandler(Percentage progress, Track track, ref double lastProgress)
        {
            int totalBlockCount = 36;
            int blockCount = progress.Value.Map(0, 100, 1, totalBlockCount);
            string newLine = (int)lastProgress == 100 ? "\n" : "";
            lastProgress = progress.Value;
            string text = $"\rDownloading... {track.Artist.Name} - {track.Title} Progress: " +
                          $"[{new string('=', blockCount)}>{new string('-', totalBlockCount - blockCount)}] " +
                          $"{progress}% {newLine}";
            
            Console.Write(text);
        }
    }
}