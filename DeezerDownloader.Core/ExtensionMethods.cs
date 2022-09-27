using System;
using System.IO;
using System.Text.RegularExpressions;
using YoutubeExplode.Common;

namespace DeezerDownloader.Core
{
    public static class ExtensionMethods
    {
        public static int Map(this double value, int fromSource, int toSource, int fromTarget, int toTarget) 
        {
            return (int)((value  - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget);
        }

        public static string NullIfEmptyOrWhiteSpace(this string str)
        {
            return !string.IsNullOrEmpty(str.Trim()) ? str : null;
        }

        public static string TryExtractFileName(string url)
        {
           return Regex.Match(url, @".+/([^?]*)").Groups[1].Value.NullIfEmptyOrWhiteSpace();
        }
    }
}