namespace DeezerDownloader.Core.Models
{
    internal class MusicBrainzRecording
    {
        public string Artist { get; set; }
        public string ArtistSort { get; set; }
        public string Title { get; set; }
        public string Album { get; set; }

        public MusicBrainzRecording(string artist, string artistSort, string title, string album)
        {
            Artist = artist;
            ArtistSort = artistSort;
            Title = title;
            Album = album;
        }
    }
}

    