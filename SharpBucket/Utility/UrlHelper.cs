using System.Text;

namespace SharpBucket.Utility
{
    internal static class UrlHelper
    {
        /// <summary>
        /// Concat url path segments until the first null or empty segment.
        /// </summary>
        public static string ConcatPathSegments(params string[] pathSegments)
        {
            var path = new StringBuilder();
            var first = true;
            foreach (var pathSegment in pathSegments)
            {
                if (string.IsNullOrEmpty(pathSegment)) break;

                if (first) first = false;
                else path.Append("/");

                path.Append(pathSegment);
            }
            return path.ToString().Replace("//", "/");
        }
    }
}
