using System;
using System.Linq;

namespace DeezerDownloader
{
    public static class Helper
    {
        public static long GetIdByParameterName(string str, string parameterName)
        {
            return Convert.ToInt64(str.Split('/')[str.Split('/').ToList().IndexOf(parameterName) + 1]);
        }
    }
}