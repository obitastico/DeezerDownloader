using Newtonsoft.Json;

namespace DeezerDownloader
{
    public class Track
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "artist")]
        public Artist Artist { get; set; }
        [JsonProperty(PropertyName = "album")]
        public Album Album { get; set; }
        [JsonProperty(PropertyName = "rank")]
        public long Rank { get; set; }

        public override string ToString()
        {
            return $"<Track Id={Id} Title={Title} Link={Link} Rank={Rank} Artist={Artist} Album={Album}>";
        }
    }
}