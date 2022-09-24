using System.Threading.Tasks;

namespace DeezerDownloader.Core
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            DeezerDownloader deezer = new DeezerDownloader();
            await deezer.DownloadUserPlaylists(3697832482, @"D:\Programmieren\RiderProjects\DeezerDownloader\out");
        }
    }
}