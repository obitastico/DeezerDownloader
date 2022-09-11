namespace DeezerDownloader.Core
{
    public static class ExtensionMethods
    {
        public static int Map(this double value, int fromSource, int toSource, int fromTarget, int toTarget) 
        {
            return (int)((value - fromSource) / (toSource - fromSource) * (toTarget - fromTarget) + fromTarget);
        }
    }
}