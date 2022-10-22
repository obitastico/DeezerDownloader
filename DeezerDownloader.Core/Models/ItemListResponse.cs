using System.Collections.Generic;
using Newtonsoft.Json;

namespace DeezerDownloader.Core.Models
{
    public class ItemListResponse<T>
    {
        [JsonProperty(PropertyName = "data")]
        public List<T> Data { get; set; }
        [JsonProperty(PropertyName = "next")]
        public string Next { get; set; }
    }
}