using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.Pocos;
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

        [Test]
        public void PostGetDeleteTag_NewTag_TagIsCreatedThenDeleted()
        {
            var tagsResource = SampleRepositories.TestRepository.RepositoryResource.TagsResource;
            var newTag = new Tag
            {
                name = "TestTag",
                message = "sample tag message",
                target = new Commit
                {
                    hash = SampleRepositories.TestRepository.RepositoryInfo.FirstCommit
                }
            };

            // POST
            var createdTag = tagsResource.PostTag(newTag);
            createdTag.ShouldBeEquivalentTo(newTag);

            // GET
            var tagFromGet = tagsResource.GetTag(newTag.name);
            tagFromGet.ShouldBeEquivalentTo(newTag);

            // DELETE
            tagsResource.DeleteTag(newTag.name);
            tagsResource.ListTags().ShouldNotContain(tag => tag.name == newTag.name);
        }

        [Test]
        public async Task PostGetDeleteTagAsync_NewTag_TagIsCreatedThenDeleted()
        {
            var tagsResource = SampleRepositories.TestRepository.RepositoryResource.TagsResource;
            var newTag = new Tag
            {
                name = "TestTagAsync",
                message = "sample tag message",
                target = new Commit
                {
                    hash = SampleRepositories.TestRepository.RepositoryInfo.FirstCommit
                }
            };

            // POST
            var createdTag = await tagsResource.PostTagAsync(newTag);
            createdTag.ShouldBeEquivalentTo(newTag);

            // GET
            var tagFromGet = await tagsResource.GetTagAsync(newTag.name);
            createdTag.ShouldBeEquivalentTo(newTag);

            // DELETE
            await tagsResource.DeleteTagAsync(newTag.name);
            tagsResource.ListTags().ShouldNotContain(tag => tag.name == newTag.name);
        }
    }
}
