using System;
using System.Net.Mime;
using TagLib;
using TagLib.Id3v2;
using TagFile = TagLib.File;

namespace DeezerDownloader.Core.Tagging
{
    internal partial class MediaFile : IDisposable
    {
        private readonly TagFile _file;

        public MediaFile(TagFile file) => _file = file;

        public void SetThumbnail(byte[] thumbnailData)
        {
            Picture pic = new Picture(thumbnailData);
            AttachmentFrame albumCoverPic = new AttachmentFrame(pic);
            albumCoverPic.MimeType = MediaTypeNames.Image.Jpeg;
            albumCoverPic.Type = PictureType.FrontCover;
            albumCoverPic.Description = "Front";

            _file.Tag.Pictures = new IPicture[] { albumCoverPic };
        }
            

        public void SetArtist(string artist) =>
            _file.Tag.Performers = new[] { artist };

        public void SetArtistSort(string artistSort) =>
            _file.Tag.PerformersSort = new[] { artistSort };

        public void SetTitle(string title) =>
            _file.Tag.Title = title;

        public void SetAlbum(string album) =>
            _file.Tag.Album = album;

        public void SetDescription(string description) =>
            _file.Tag.Description = description;

        public void SetComment(string comment) =>
            _file.Tag.Comment = comment;

        public void Dispose()
        {
            _file.Tag.DateTagged = DateTime.Now;
            _file.Save();
            _file.Dispose();
        }
    }

    internal partial class MediaFile
    {
        public static MediaFile Create(string filePath)
        {
            return new MediaFile(TagFile.Create(filePath));
        }
    }
}

    