using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeezerDownloader.Core
{
    public class TracksResponse
    {
        [JsonProperty(PropertyName = "data")]
        public List<Track> Data { get; set; }
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
    }
}