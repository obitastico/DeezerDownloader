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

        public DeezerClient()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(BaseUrl);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public List<Playlist> GetPlaylistsByUserId(long userId)
        {
            List<Playlist> playlists = new List<Playlist>();

            PlaylistsResponse playlistsResponse;
            string url = $"user/{userId}/playlists";

            do
            {
                HttpResponseMessage response = Client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                    return null;

                string sResponse = response.Content.ReadAsStringAsync().Result;

                var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                playlistsResponse = JsonConvert.DeserializeObject<PlaylistsResponse>(sResponse, settings);

                if (playlistsResponse == null)
                    return null;
                
                playlists.AddRange(playlistsResponse.Data);
                url = playlistsResponse.Next;

            } while (playlistsResponse.Next != null);
            
            return playlists;
        }

        public Playlist GetPlaylistById(long playlistId)
        {
            HttpResponseMessage response = Client.GetAsync($"/playlist/{playlistId}").Result;
            if (!response.IsSuccessStatusCode)
                return null;
            
            string sResponse = response.Content.ReadAsStringAsync().Result;
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.DeserializeObject<Playlist>(sResponse, settings);
        }

        public List<Track> GetTracksFromPlaylistId(long playlistId)
        {
            List<Track> tracks = new List<Track>();

            TracksResponse tracksResponse;
            string url = $"playlist/{playlistId}/tracks";

            do
            {
                HttpResponseMessage response = Client.GetAsync(url).Result;
                if (!response.IsSuccessStatusCode)
                    return null;

                string sResponse = response.Content.ReadAsStringAsync().Result;

                var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
                tracksResponse = JsonConvert.DeserializeObject<TracksResponse>(sResponse, settings);

                if (tracksResponse == null)
                    return null;
                
                tracks.AddRange(tracksResponse.Data);
                url = tracksResponse.Next;

            } while (tracksResponse.Next != null);
            
            return tracks;
        }

        public Artist GetArtistById(long artistId)
        {
            HttpResponseMessage response = Client.GetAsync($"artist/{artistId}").Result;
            if (!response.IsSuccessStatusCode)
                return null;

            string sResponse = response.Content.ReadAsStringAsync().Result;
            var settings = new JsonSerializerSettings { DateFormatString = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.DeserializeObject<Artist>(sResponse, settings);
        }

        public Track GetTrackById(long trackId)
        {
            HttpResponseMessage response = Client.GetAsync($"track/{trackId}").Result;
            if (!response.IsSuccessStatusCode)
                return null;

            string sResponse = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<Track>(sResponse);
        }
    }
}