using SharpBucket.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.Pocos
{
    public static class RepositoryAssertions
    {
        public static Repository ShouldBeFilled(this Repository repository)
        {
            repository.ShouldNotBeNull();
            repository.created_on.ShouldNotBeNullOrEmpty();
            repository.description.ShouldNotBeNull("description could be empty");
            repository.fork_policy.ShouldNotBeNullOrEmpty();
            repository.full_name.ShouldNotBeNullOrEmpty();
            repository.has_issues.ShouldNotBeNull();
            repository.has_wiki.ShouldNotBeNull();
            repository.is_private.ShouldNotBeNull();
            repository.language.ShouldNotBeNull("language could be empty");
            repository.links.ShouldBeFilled();
            repository.mainbranch.ShouldBeFilled();
            repository.name.ShouldNotBeNullOrEmpty();
            repository.owner.ShouldBeFilled();
            ////repository.parent could be null if it's not a fork
            ////repository.project could be null if it do not belong to a project
            repository.scm.ShouldNotBeNullOrEmpty();
            repository.size.ShouldNotBeNull();
            repository.slug.ShouldNotBeNullOrEmpty();
            repository.updated_on.ShouldNotBeNullOrEmpty();
            repository.uuid.ShouldNotBeNullOrEmpty();
            ////repository.website could be null

            return repository;
        }
    }
}
