using System.Threading.Tasks;

namespace DeezerDownloader.Core
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Downloader deezer = new Downloader();
            await deezer.DownloadAlbum("OUTPUT_PATH", 00000);
        }
    }
}