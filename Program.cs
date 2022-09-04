using System.Threading.Tasks;

namespace DeezerDownloader
{
    internal class Program
    {
        public static async Task Main(string[] args)
        {
            DeezerDownloader deezer = new DeezerDownloader();
            await deezer.DownloadPlaylist(9861281962);
        }
    }
}