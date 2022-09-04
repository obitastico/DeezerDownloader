using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeezerDownloader.Core
{
    public class PlaylistsResponse
    {
        [JsonProperty(PropertyName = "data")]
        public List<Playlist> Data { get; set; }
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
    }
}