using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DeezerDownloader.Core
{
    public class Playlist
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }
        [JsonProperty(PropertyName = "collaborative")]
        public bool Collaborative { get; set; }
        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }
        [JsonProperty(PropertyName = "tracklist")]
        public string Tracklist { get; set; }
        [JsonProperty(PropertyName = "creation_date")]
        public DateTime CreationDate { get; set; }
        [JsonProperty(PropertyName = "tracks")]
        public TracksResponse Tracks { get; set; }

        public override string ToString()
        {
            return $"<Playlist Id={Id} Title={Title} Duration={Duration} Collaborative={Collaborative} Link={Link} " +
                   $"Tracklist={Tracklist} CreationDate={CreationDate}>";
        }
    }
}