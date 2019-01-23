using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    public class ListParametersTests
    {
        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ToDictionary_Should_Work(ListParameters input, Dictionary<string, object> expected)
        {
            var output = input.ToDictionary();
            output.ShouldBe(expected);
        }

        public static IEnumerable TestCases()
        {
            var filterGuid = Guid.NewGuid().ToString();
            var sortGuid = Guid.NewGuid().ToString();
            var max = new Random().Next(100);

            return new[]
            {
                new TestCaseData(
                    new ListParameters(),
                    null
                ),

                new TestCaseData(
                    new ListParameters() { Max = max },
                    null
                ),

                new TestCaseData(
                    new ListParameters() { Filter = filterGuid },
                    new Dictionary<string, object>() { { "q", filterGuid } }
                ),

                new TestCaseData(
                    new ListParameters() { Filter = filterGuid, Max = max },
                    new Dictionary<string, object>() { { "q", filterGuid } }
                ),

                new TestCaseData(
                    new ListParameters() { Sort = sortGuid },
                    new Dictionary<string, object>() { { "sort", sortGuid } }
                ),

                new TestCaseData(
                    new ListParameters() { Sort = sortGuid, Max = max },
                    new Dictionary<string, object>() { { "sort", sortGuid } }
                ),

                new TestCaseData(
                    new ListParameters() { Filter = filterGuid, Sort = sortGuid, Max = max },
                    new Dictionary<string, object>() { { "q", filterGuid }, { "sort", sortGuid } }
                ),
            };
        }
    }
}
