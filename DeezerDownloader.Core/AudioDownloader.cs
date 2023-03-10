using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
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

        public async Task DownloadAudioAsnyc(Track track, string filePath, IProgress<double> progress, CancellationToken cancellationToken = default)
        {
            string sQuery = $"{track.Artist.Name} - {track.Title}";
            VideoSearchResult videoInfo = (await Youtube.Search.GetVideosAsync(sQuery).CollectAsync(1)).FirstOrDefault();

            if (videoInfo == null)
                throw new Exception("Cannot get Video from Youtube");

            var manifest = await Youtube.Videos.Streams.GetManifestAsync(videoInfo.Id, cancellationToken);
            IStreamInfo streamInfo = manifest
                .GetAudioStreams()
                .Where(x => x.Container.Name == "mp4")
                .OrderBy(x => x.Size)
                .FirstOrDefault();

            var dirPath = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrWhiteSpace(dirPath))
                Directory.CreateDirectory(dirPath);

            await Youtube.Videos.DownloadAsync(
                new[] { streamInfo }, 
                new ConversionRequestBuilder(filePath)
                    .SetContainer(Container.Mp3)
                    .SetPreset(ConversionPreset.UltraFast)
                    .Build(),
                progress,
                cancellationToken
            );
            
            await TagInjector.InjectTagsAsync(filePath, videoInfo);
        }
    }
}

