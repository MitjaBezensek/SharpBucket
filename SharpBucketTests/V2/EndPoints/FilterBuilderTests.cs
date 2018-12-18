using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void ParseSingleQuotedString_ForGivenInputAndExpected(string input, string expected)
        {
            var output = FilterBuilder.ParseSingleQuotedString(input);
            output.ShouldBe(expected);
        }
    }
}
