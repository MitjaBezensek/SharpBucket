using System;
using System.Collections;
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
    public class DiffParametersTests
    {
        [Test]
        [TestCaseSource(nameof(TestCases))]
        public void ToDictionary_Should_Work(DiffParameters input, Dictionary<string, object> expected)
        {
            var output = input.ToDictionary();
            output.ShouldBe(expected);
        }


        public static IEnumerable TestCases()
        {
            var guid = Guid.NewGuid().ToString();
            var context = (byte)new Random().Next(5, 200);

            return new[]
            {
                new TestCaseData(
                    new DiffParameters(),
                    null
                ),

                new TestCaseData(
                    new DiffParameters() { Context = context },
                    new Dictionary<string, object>() { { "context", context } }
                ),

                new TestCaseData(
                    new DiffParameters() { Path = guid },
                    new Dictionary<string, object>() { { "path", guid } }
                ),

                new TestCaseData(
                    new DiffParameters() { IgnoreWhitespace = true },
                    new Dictionary<string, object>() { { "ignore_whitespace", "true" } }
                ),

                new TestCaseData(
                    new DiffParameters() { Binary = false },
                    new Dictionary<string, object>() { { "binary", "false" } }
                ),

                new TestCaseData(
                    new DiffParameters() { Context = context, Path = guid },
                    new Dictionary<string, object>() { { "context", context }, { "path", guid } }
                ),

                new TestCaseData(
                    new DiffParameters() { Context = context, Path = guid, IgnoreWhitespace = true, Binary = false },
                    new Dictionary<string, object>() { { "context", context }, { "path", guid }, { "ignore_whitespace", "true" }, { "binary", "false" } }
                ),
            };
        }

    }
}
