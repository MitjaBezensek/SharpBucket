using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class FilterBuilderTests
    {
        [TestCase("foo=bar", "foo=bar")]
        [TestCase("foo='bar'", "foo=\"bar\"")]
        [TestCase("foo='bob''s burgers'", "foo=\"bob's burgers\"")]
        [TestCase("", "")]
        [TestCase(null, null)]
        [TestCase("message = 'Need to test \"foo\"'", "message = \"Need to test \\\"foo\\\"\"")]
        [TestCase(@"message = 'Need to test ""foo""'", "message = \"Need to test \\\"foo\\\"\"")]
        public void ParseSingleQuotedString_ForGivenInputAndExpected(string input, string expected)
        {
            var output = FilterBuilder.ParseSingleQuotedString(input);
            output.ShouldBe(expected);
        }
    }
}
