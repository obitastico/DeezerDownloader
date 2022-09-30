using Newtonsoft.Json;

namespace DeezerDownloader.Core.Models
{
    public class Album
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "tracklist")]
        public string Tracklist { get; set; }
        [JsonProperty(PropertyName = "artist")]
        public Artist Artist { get; set; }
        [JsonProperty(PropertyName = "tracks")]
        public TracksResponse Tracks { get; set; }

        public override string ToString()
        {
            return $"<Album Id={Id} Title={Title} Artist<{Artist}> Tracklist={Tracklist}>";
        }
    }
}