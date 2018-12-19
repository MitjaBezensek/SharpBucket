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

        [TestCase("d12fbb96-2a37-4c1d-adf8-8d5da44d8f40", true, "{d12fbb96-2a37-4c1d-adf8-8d5da44d8f40}")]
        [TestCase("{d12fbb96-2a37-4c1d-adf8-8d5da44d8f40}", true, "{d12fbb96-2a37-4c1d-adf8-8d5da44d8f40}")]
        [TestCase("d12fbb962a374c1dadf88d5da44d8f40", false, null)]
        [TestCase("foo", false, null)]
        [TestCase("", false, null)]
        [TestCase(null, false, null)]
        public void TryGetGuid_ShouldWork(string input, bool expectedResult, string expectedOutput)
        {
            var result = input.TryGetGuid(out var output);
            result.ShouldBe(expectedResult);
            output.ShouldBe(expectedOutput);
        }

        [TestCase("ab806147-1e17-4e8d-b350-9b7bac807ef0", "{ab806147-1e17-4e8d-b350-9b7bac807ef0}")]
        [TestCase("{ab806147-1e17-4e8d-b350-9b7bac807ef0}", "{ab806147-1e17-4e8d-b350-9b7bac807ef0}")]
        [TestCase("ab8061471e174e8db3509b7bac807ef0", "ab8061471e174e8db3509b7bac807ef0")]
        [TestCase("foo", "foo")]
        [TestCase("", "")]
        [TestCase(null, null)]
        public void GuidOrValue_ShouldWork(string input, string expected)
        {
            var output = input.GuidOrValue();
            output.ShouldBe(expected);
        }
    }
}
