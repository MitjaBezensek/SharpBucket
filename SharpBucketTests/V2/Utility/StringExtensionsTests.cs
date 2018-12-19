using NUnit.Framework;
using SharpBucket.Utility;
using Shouldly;

namespace SharpBucketTests.V2.Utility
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestCase("abc", "abc")]
        [TestCase("abc-123", "abc-123")]
        [TestCase("ABC-123", "abc-123")]
        [TestCase("Bob's Repo", "bobs-repo")]
        [TestCase("d12fbb96-2a37-4c1d-adf8-8d5da44d8f40", "{d12fbb96-2a37-4c1d-adf8-8d5da44d8f40}")]
        [TestCase("{d12fbb96-2a37-4c1d-adf8-8d5da44d8f40}", "{d12fbb96-2a37-4c1d-adf8-8d5da44d8f40}")]
        [TestCase("double--hyphen", "double-hyphen")]
        [TestCase("Bob'''''s Repo", "bobs-repo")]
        [TestCase("d12fbb962a374c1dadf88d5da44d8f40", "d12fbb962a374c1dadf88d5da44d8f40")]
        [TestCase("My \"test\" repo", "my-test-repo")]
        public void ToSlug_ShouldMatchExpected(string input, string expected)
        {
            var output = input.ToSlug();
            output.ShouldBe(expected);
        }
    }
}
