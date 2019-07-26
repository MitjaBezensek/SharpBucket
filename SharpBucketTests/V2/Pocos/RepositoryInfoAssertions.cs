using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class RepositoryInfoAssertions
    {
        public static RepositoryInfo ShouldBeFilled(this RepositoryInfo repository)
        {
            repository.ShouldNotBeNull();
            repository.full_name.ShouldNotBeNullOrEmpty();
            repository.links.ShouldBeFilled();
            repository.name.ShouldNotBeNullOrEmpty();
            repository.uuid.ShouldNotBeNullOrEmpty();

            return repository;
        }
    }
}