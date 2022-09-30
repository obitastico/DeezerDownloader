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

        public List<Playlist> GetPlaylistsByUserId(long userId)
        {
            List<Playlist> playlists = new List<Playlist>();

            PlaylistsResponse playlistsResponse;
            string url = $"user/{userId}/playlists";

            do
            {
                
                playlistsResponse = JsonConvert.DeserializeObject<PlaylistsResponse>(GetReponseString(url), Settings);

                if (playlistsResponse == null)
                    return null;
                
                playlists.AddRange(playlistsResponse.Data);
                url = playlistsResponse.Next;

            } while (playlistsResponse.Next != null);
            
            return playlists;
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

        public List<Track> GetTracksFromPlaylistId(long playlistId)
        {
            List<Track> tracks = new List<Track>();

            TracksResponse tracksResponse;
            string url = $"playlist/{playlistId}/tracks";

            do
            {
                tracksResponse = JsonConvert.DeserializeObject<TracksResponse>(GetReponseString(url), Settings);

                if (tracksResponse == null)
                    return null;
                
                tracks.AddRange(tracksResponse.Data);
                url = tracksResponse.Next;

            } while (tracksResponse.Next != null);
            
            return tracks;
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
        
        public string GetReponseString(string url)
        {
            HttpResponseMessage response = Client.GetAsync(url).Result;
            if (!response.IsSuccessStatusCode)
                return null;
            
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}