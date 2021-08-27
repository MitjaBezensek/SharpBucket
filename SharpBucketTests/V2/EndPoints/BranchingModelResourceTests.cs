using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class BranchingModelResourceTests
    {
        [Test]
        public void GetBranchingModel_NotConfiguredRepository_ReturnDefaultBranchingModel()
        {
            var repo = SampleRepositories.EmptyTestRepository;
            var branchingModel = repo.BranchingModelResource.GetBranchingModel();
            branchingModel.ShouldBeFilled();

            // Also check default value for release branch type to ensure that
            // reset after PutSettings tests are valid
            branchingModel.branch_types.ShouldContain(p => p.kind == "release" && p.prefix == "release/");
        }

        [Test]
        public async Task GetBranchingModelAsync_NotConfiguredRepository_ReturnDefaultBranchingModel()
        {
            var repo = SampleRepositories.EmptyTestRepository;
            var branchingModel = await repo.BranchingModelResource.GetBranchingModelAsync();
            branchingModel.ShouldBeFilled();

            // Also check default value for release branch type to ensure that
            // reset after PutSettings tests are valid
            branchingModel.branch_types.ShouldContain(p => p.kind == "release" && p.prefix == "release/");
        }

        [Test]
        public void GetSettings_NotConfiguredRepository_ReturnDefaultSettings()
        {
            var repo = SampleRepositories.EmptyTestRepository;
            var branchingModelSettings = repo.BranchingModelResource.GetSettings();
            branchingModelSettings.ShouldBeFilled();
        }

        [Test]
        public async Task GetSettingsAsync_NotConfiguredRepository_ReturnDefaultSettings()
        {
            var repo = SampleRepositories.EmptyTestRepository;
            var branchingModelSettings = await repo.BranchingModelResource.GetSettingsAsync();
            branchingModelSettings.ShouldBeFilled();
        }

        [Test]
        public void PutSettings_NotConfiguredRepository_ReturnDefaultSettings()
        {
            var repo = SampleRepositories.EmptyTestRepository;
            var settings = new BranchingModelSettings
            {
                branch_types = new List<BranchingModelSettingsBranchType>
                {
                    new BranchingModelSettingsBranchType
                    {
                        kind = "release",
                        prefix = "delivered/",
                        enabled = true,
                    },
                },
            };
            var branchingModelSettings = repo.BranchingModelResource.PutSettings(settings);
            branchingModelSettings.ShouldBeFilled();
            branchingModelSettings.branch_types.ShouldContain(p => p.kind == "release" && p.prefix == "delivered/");

            // put back default value
            settings = new BranchingModelSettings
            {
                branch_types = new List<BranchingModelSettingsBranchType>
                {
                    new BranchingModelSettingsBranchType
                    {
                        kind = "release",
                        prefix = "release/",
                        enabled = true,
                    },
                },
            };
            repo.BranchingModelResource.PutSettings(settings);
        }

        [Test]
        public async Task PutSettingsAsync_NotConfiguredRepository_ReturnDefaultSettings()
        {
            var repo = SampleRepositories.EmptyTestRepository;
            var settings = new BranchingModelSettings
            {
                branch_types = new List<BranchingModelSettingsBranchType>
                {
                    new BranchingModelSettingsBranchType
                    {
                        kind = "release",
                        prefix = "delivered/",
                        enabled = true,
                    },
                },
            };
            var branchingModelSettings = await repo.BranchingModelResource.PutSettingsAsync(settings);
            branchingModelSettings.ShouldBeFilled();
            branchingModelSettings.branch_types.ShouldContain(p => p.kind == "release" && p.prefix == "delivered/");

            // put back default value
            settings = new BranchingModelSettings
            {
                branch_types = new List<BranchingModelSettingsBranchType>
                {
                    new BranchingModelSettingsBranchType
                    {
                        kind = "release",
                        prefix = "release/",
                        enabled = true,
                    },
                },
            };
            await repo.BranchingModelResource.PutSettingsAsync(settings);
        }
    }
}
