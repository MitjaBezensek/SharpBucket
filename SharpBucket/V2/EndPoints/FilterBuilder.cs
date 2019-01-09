using System;
using System.Text.RegularExpressions;

namespace SharpBucket.V2.EndPoints
{
    public static class FilterBuilder
    {
        /// <summary>
        /// Takes a string with SQL single-quoting conventions and converts it to double-quoting
        /// per the Bitbucket API.
        /// </summary>
        /// <param name="input">A string with single-quoted text, like <code>foo='bob''s burgers'</code></param>
        /// <returns>The string with single quotes converted to double quotes, like <code>foo="bob's burgers"</code></returns>
        public static string ParseSingleQuotedString(string input)
        {
            if (input?.Contains("'") == true)
            {
                input = input.Replace("\"", "\\\"");
                var singleQuoteRegex = new Regex(@"''|'");
                return singleQuoteRegex.Replace(input, m => m.Length == 2 ? "'" : "\"");
            }
            else
                return input;
        }

        /// <summary>
        /// Produces a date/time string in ISO-8601 format.
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FormatDateTime(DateTime input) => input.ToString("o");
    }
}
