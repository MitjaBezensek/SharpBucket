﻿using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;
using SharpBucketTests.V2.Pocos;
using Shouldly;

namespace SharpBucketTests.V2.EndPoints
{
    [TestFixture]
    class ProjectsResourceTests
    {
        private ProjectsResource UserWorkspaceProjectsResource { get; set; }

        [OneTimeSetUp]
        public void Init()
        {
            var sharpBucket = TestHelpers.SharpBucketV2;
            var workspacesEndPoint = sharpBucket.WorkspacesEndPoint();

            var userWorkspace = workspacesEndPoint
                                .EnumerateWorkspaces(new EnumerateWorkspacesParameters { PageLen = 1 })
                                .First();
            this.UserWorkspaceProjectsResource = workspacesEndPoint
                                                 .WorkspaceResource(userWorkspace.slug)
                                                 .ProjectsResource;
        }

        [Test]
        public void ListProjects_AfterHavingCreateOneProjectInWorkspace_ShouldReturnAtLestTheCreatedProject()
        {
            var projectKey = "Test_" + Guid.NewGuid().ToString("N"); // must start by a letter
            var project = new Project
            {
                key = projectKey,
                name = "Name of " + projectKey,
                is_private = true,
                description = $"project created by the unit test {TestContext.CurrentContext.Test.FullName}",
            };
            project = UserWorkspaceProjectsResource.PostProject(project);

            try
            {
                var projects = UserWorkspaceProjectsResource.ListProjects();
                projects.ShouldNotBeEmpty();
                projects.Any(r => r.name == project.name).ShouldBe(true);
                projects.Select(p => p.ShouldBeFilled())
                    .Any(r => r.name == project.name).ShouldBe(true);

                // also quickly check other sync methods here to avoid to create and delete to much projects
                projects = UserWorkspaceProjectsResource.ListProjects(new ListParameters { Filter = "name ~ \"nomatchexpected\"" });
                projects.ShouldBeEmpty();

                projects = UserWorkspaceProjectsResource.EnumerateProjects().ToList();
                projects.ShouldNotBeEmpty();
            }
            finally
            {
                UserWorkspaceProjectsResource.ProjectResource(project.key).DeleteProject();
            }
        }

        [Test]
        public async Task EnumerateProjectsAsync_AfterHavingCreateOneProjectInTeam_ShouldReturnAtLestTheCreatedProject()
        {
            var projectKey = "Test_" + Guid.NewGuid().ToString("N"); // must start by a letter
            var project = new Project
            {
                key = projectKey,
                name = "Name of " + projectKey,
                is_private = true,
                description = "project created by the unit test ListProjects_AfterHavingAddAProject_ShouldReturnAtLestTheCreatedProject"
            };
            project = await UserWorkspaceProjectsResource.PostProjectAsync(project);

            try
            {
                var projects = await UserWorkspaceProjectsResource.EnumerateProjectsAsync().ToListAsync();
                projects.ShouldNotBeEmpty();
                projects.Any(r => r.name == project.name).ShouldBe(true);
                projects.Select(p => p.ShouldBeFilled())
                    .Any(r => r.name == project.name).ShouldBe(true);
            }
            finally
            {
                await UserWorkspaceProjectsResource.ProjectResource(project.key).DeleteProjectAsync();
            }
        }
    }
}