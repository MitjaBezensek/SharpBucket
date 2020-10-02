using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class TagsResourceTests
    {
        [Test]
        public void ListTags_ExistingRepositoryWithTags_ReturnSomeTags()
        {
            var tagsResource = SampleRepositories.TestRepository.RepositoryResource.TagsResource;

            var tags = tagsResource.ListTags();

            tags.ShouldNotBeNull();
            tags.Count.ShouldBe(2);
            tags[0].ShouldBeFilled();
        }

        [Test]
        public void EnumerateTags_ExistingRepositoryWithTags_ReturnSomeTags()
        {
            var tagsResource = SampleRepositories.TestRepository.RepositoryResource.TagsResource;

            var tags = tagsResource.EnumerateTags().ToList();

            tags.ShouldNotBeNull();
            tags.Count.ShouldBe(2);
            tags[0].ShouldBeFilled();
        }

        [Test]
        public async Task EnumerateTagsAsync_ExistingRepositoryWithTags_ReturnSomeTags()
        {
            var tagsResource = SampleRepositories.TestRepository.RepositoryResource.TagsResource;

            var tags = await tagsResource.EnumerateTagsAsync().ToListAsync();

            tags.ShouldNotBeNull();
            tags.Count.ShouldBe(2);
            tags[0].ShouldBeFilled();
        }
    }
}
