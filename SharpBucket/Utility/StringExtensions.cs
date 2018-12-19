using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace SharpBucket.Utility
{
    public static class StringExtensions
    {
        /// <summary>
        /// Converts a repository name into the corresponding slug.
        /// If a UUID in B or D format is passed in, it will be returned in B format.
        /// </summary>
        /// <param name="repoName">The name of a repository.</param>
        /// <returns></returns>
        public static string ToSlug(this string repoName)
        {
            if (Guid.TryParseExact(repoName, "B", out _))
                return repoName;
            if (Guid.TryParseExact(repoName, "D", out _))
                return $"{{{repoName}}}";

            if (repoName.Contains('\''))
                repoName = repoName.Replace("'", "");
            var slugRegex = new Regex(@"[^a-zA-Z\.\-_0-9]+|-{2,}");
            return slugRegex.Replace(repoName, "-").ToLowerInvariant();
        }
    }
}
