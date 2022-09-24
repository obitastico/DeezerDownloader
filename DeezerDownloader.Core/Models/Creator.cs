using Newtonsoft.Json;

namespace DeezerDownloader.Core.Models
{
    public class Creator
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "tracklist")]
        public string Tracklist { get; set; }

        public override string ToString()
        {
            return $"<Creator Id={Id} Title={Name} Tracklist={Tracklist}>";
        }
    }
}