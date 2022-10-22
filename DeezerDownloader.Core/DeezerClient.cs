using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using DeezerDownloader.Core.Models;
using Newtonsoft.Json;

namespace DeezerDownloader.Core
{
    public class DeezerClient
    {
        private const string BaseUrl = "https://api.deezer.com";
        private HttpClient Client { get; }

        private static readonly JsonSerializerSettings Settings = new() { DateFormatString = "yyyy-MM-dd HH:mm:ss" };

        public DeezerClient()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public List<T> GetItemList<T>(string url)
        {
            ItemListResponse<T> itemListResponse;
            List<T> itemList = new List<T>();

            do
            {
                itemListResponse = JsonConvert.DeserializeObject<ItemListResponse<T>>(GetReponseString(url), Settings);

                if (itemListResponse == null)
                    return itemList;
                
                itemList.AddRange(itemListResponse.Data);
                url = itemListResponse.Next;

            } while (itemListResponse.Next != null);
            
            return itemList;
        }
        
        public List<Track> GetFavouriteTracksByUserId(long userId)
        {
            string url = $"user/{userId}/tracks";
            
            return GetItemList<Track>(url);
        }

        public List<Track> GetTracksFromPlaylistId(long playlistId)
        {
            string url = $"playlist/{playlistId}/tracks";
            
            return GetItemList<Track>(url);
        }
        
        public List<Playlist> GetPlaylistsByUserId(long userId)
        {
            string url = $"user/{userId}/playlists";

            return GetItemList<Playlist>(url);
        }

        public Album GetAlbumById(long albumId)
        {
            var url = $"/album/{albumId}";
            return JsonConvert.DeserializeObject<Album>(GetReponseString(url), Settings);
        }

        public Playlist GetPlaylistById(long playlistId)
        {
            var url = $"/playlist/{playlistId}";
            return JsonConvert.DeserializeObject<Playlist>(GetReponseString(url), Settings);
        }

        public Artist GetArtistById(long artistId)
        {
            var url = $"artist/{artistId}";
            return JsonConvert.DeserializeObject<Artist>(GetReponseString(url), Settings);
        }

        public Track GetTrackById(long trackId)
        {
            var url = $"track/{trackId}";
            return JsonConvert.DeserializeObject<Track>(GetReponseString(url));
        }
        
        public Creator GetCreatorByid(long userId)
        {
            var url = $"/user/{userId}";
            return JsonConvert.DeserializeObject<Creator>(GetReponseString(url));
        }
        
        public string GetReponseString(string url)
        {
            HttpResponseMessage response = Client.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
                return null;
            
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}