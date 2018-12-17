using System.Text.RegularExpressions;

namespace SharpBucket.V2.EndPoints
{
    public static class FilterBuilder
    {
        /// <summary>
        /// Take a string with SQL single-quoting conventions and convert it to double-quoting
        /// per the Bitbucket API.
        /// </summary>
        /// <param name="input">A string with single-quoted text, like <code>foo='bob''s burgers'</code></param>
        /// <returns>The string with single quotes converted to double quotes, like <code>foo="bob's burgers"</code></returns>
        public static string ParseSingleQuotedString(string input)
        {
            if (input?.Contains("'") == true)
            {
                var singleQuoteRegex = new Regex(@"''|'");
                return singleQuoteRegex.Replace(input, m => m.Length == 2 ? "'" : "\"");
            }
            else
                return input;
        }
    }
}
