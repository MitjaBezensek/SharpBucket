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
        /// <returns>The slug version of the repository name, or the formatted UUID.</returns>
        public static string ToSlug(this string repoName)
        {
            if (repoName.TryGetGuid(out var guidString))
                return guidString;

            if (repoName.Contains('\''))
                repoName = repoName.Replace("'", "");
            var slugRegex = new Regex(@"[^a-zA-Z\.\-_0-9]+|-{2,}");
            return slugRegex.Replace(repoName, "-").ToLowerInvariant();
        }

        /// <summary>
        /// Determines whether the input string is a Guid in B or D format.
        /// </summary>
        /// <param name="input">The string to test.</param>
        /// <param name="guidString">The Guid in B format, or null.</param>
        /// <returns>True if the input string is a Guid in B or D format, or false.</returns>
        public static bool TryGetGuid(this string input, out string guidString)
        {
            if (Guid.TryParseExact(input, "B", out _))
            {
                guidString = input;
                return true;
            }
            else if (Guid.TryParseExact(input, "D", out _))
            {
                guidString = $"{{{input}}}";
                return true;
            }
            else
            {
                guidString = null;
                return false;
            }
        }

        /// <summary>
        /// Returns a formatted Guid if the input string is a Guid in B or D format,
        /// or the input string if not.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string GuidOrValue(this string input)
        {
            return input.TryGetGuid(out var guidString) ? guidString : input;
        }
    }
}
