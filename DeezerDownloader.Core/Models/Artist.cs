using Newtonsoft.Json;

namespace DeezerDownloader.Core.Models
{
    public class Artist
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "tracklist")]
        public string Tracklist { get; set; }

        public override string ToString()
        {
            return $"<Artist Id={Id} Name={Name} Link={Link} Tracklist={Tracklist}>";
        }
    }
}