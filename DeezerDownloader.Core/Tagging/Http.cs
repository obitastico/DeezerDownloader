using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;

namespace DeezerDownloader.Core.Tagging
{
    internal static class Http
    {
        public static HttpClient Client { get; } = new()
        {
            DefaultRequestHeaders =
            {
                UserAgent =
                {
                    new ProductInfoHeaderValue(
                        "YoutubeDownloader",
                        Assembly.GetExecutingAssembly().GetName().Version.ToString(3)
                    )
                }
            }
        };
    }
}

    