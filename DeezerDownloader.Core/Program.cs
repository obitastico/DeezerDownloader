using System.Threading.Tasks;

namespace DeezerDownloader.Core
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            Downloader deezer = new Downloader();
            await deezer.DownloadAlbum(@"D:\Programmieren\RiderProjects\DeezerDownloader\out", 302127);
        }
    }
}