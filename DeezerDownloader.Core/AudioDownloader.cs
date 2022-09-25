using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DeezerDownloader.Core.Tagging;
using Gress;
using TagLib.Matroska;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Search;
using YoutubeExplode.Videos.Streams;
using YoutubeExplode.Converter;
using Track = DeezerDownloader.Core.Models.Track;

namespace DeezerDownloader.Core
{
    public class AudioDownloader
    {
        private YoutubeClient Youtube { get; }
        private MediaTagInjector TagInjector { get; }

        public AudioDownloader()
        {
            Youtube = new YoutubeClient(Http.Client);
            TagInjector = new MediaTagInjector();
        }

        public async Task DownloadAudioAsnyc(Track track, string filePath, Progress<double> progress)
        {
            string sQuery = $"{track.Artist.Name} - {track.Title}";
            VideoSearchResult videoInfo = (await Youtube.Search.GetVideosAsync(sQuery).CollectAsync(1)).FirstOrDefault();

            if (videoInfo == null)
                throw new Exception("Cannot get Video from Youtube");

            var manifest = await Youtube.Videos.Streams.GetManifestAsync(videoInfo.Id);
            IStreamInfo streamInfo = manifest.GetAudioStreams().GetWithHighestBitrate();
            
            var dirPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dirPath))
                Directory.CreateDirectory(dirPath);

            // await Youtube.Videos.DownloadAsync(
            //     new[] { streamInfo }, 
            //     new ConversionRequestBuilder(filePath)
            //         .SetContainer(Container.Mp3)
            //         .SetPreset(ConversionPreset.UltraFast)
            //         .Build(),
            //     progress
            // );
            
            await TagInjector.InjectTagsAsync(filePath, videoInfo);
        }
    }
}

