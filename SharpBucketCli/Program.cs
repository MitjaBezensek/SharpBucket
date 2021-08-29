using System;
using System.Linq;
using SharpBucket.V2;
using SharpBucket.V2.EndPoints;
using SharpBucket.V2.Pocos;

namespace SharpBucketCli
{
    /// <summary>
    /// This program is both a sample and a tool to help SharpBucket developers to maintain their test account.
    /// When developing on SharpBucket you may quickly generate a lot of repositories which are not cleaned up because
    /// due to broken unit test execution during a debug session, or writing new unit tests that leak, and so on.
    /// And deleting a lot of repositories with the web interface is ungrateful... 
    /// </summary>
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    var program = new Program();
                    program.ListenToInteractiveCommands();
                    return 0;
                }

                Console.Error.WriteLine("Non interactive mode is not yet implemented");
                return -1;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return -1;
            }
        }

        private SharpBucketV2 SharpBucket { get; }

        /// <summary>
        /// The account on which I am currently logged.
        /// </summary>
        private User Me { get; set; }

        /// <summary>
        /// The workspace On which I am currently working on.
        /// </summary>
        private Workspace Workspace { get; set; }

        private Program()
        {
            this.SharpBucket = new SharpBucketV2();
        }

        private void ListenToInteractiveCommands()
        {
            Console.WriteLine("Welcome to the interactive mode of the SharpBucket Command Line Interface");
            this.UseEnvironmentCredentials();
            while (true)
            {
                Console.Write($"{this.Me?.nickname}:{this.Workspace?.slug}> ");
                var command = Console.ReadLine() ?? string.Empty;
                var args = command.Split(' ');
                var verb = args[0];
                var options = args.Skip(1).ToArray();

                try
                {
                    switch (verb)
                    {
                        case "help": Help(); break;
                        case "clean": Clean(); break;
                        case "list": List(options); break;
                        case "switch": Switch(options); break;
                        case "exit": return;
                        default: Console.WriteLine("Unrecognized command. Type help to get help about existing commands"); break;
                    }
                }
                catch (Exception e)
                {
                    Console.Error.WriteLine(e);
                }
            }
        }

        private void UseEnvironmentCredentials()
        {
            var consumerKey = System.Environment.GetEnvironmentVariable("SB_CONSUMER_KEY");
            var consumerKeySecret = System.Environment.GetEnvironmentVariable("SB_CONSUMER_SECRET_KEY");

            if (!string.IsNullOrEmpty(consumerKey) && !string.IsNullOrEmpty(consumerKeySecret))
            {
                this.SharpBucket.OAuth2ClientCredentials(consumerKey, consumerKeySecret);
                this.Me = this.SharpBucket.UserEndPoint().GetUser();
                this.Workspace = this.SharpBucket.WorkspacesEndPoint()
                                     .EnumerateWorkspaces(new EnumerateWorkspacesParameters {PageLen = 1})
                                     .First();
                Console.WriteLine($"You have been automatically logged as {Me.display_name}");
            }
        }

        private static void Help()
        {
            Console.WriteLine("Available commands are:");
            Console.WriteLine("  clean     : Delete all the repositories in the current workspace.");
            Console.WriteLine("              Useful to clean up a test account overwhelmed by repositories not");
            Console.WriteLine("              correctly cleaned up by the unit tests.");
            Console.WriteLine();
            Console.WriteLine("  list     : List workspaces or repositories in the current workspace.");
            Console.WriteLine();
            Console.WriteLine("  switch   : Switch to another workspace.");
            Console.WriteLine();
            Console.WriteLine("  exit     : Exit the interactive mode.");
            Console.WriteLine();
            Console.WriteLine("  help     : Print this help.");
        }

        private void Clean()
        {
            if (this.Me == null)
            {
                Console.Error.WriteLine("You must be logged to execute that command");
                return;
            }

            var repositoriesResource = this.SharpBucket.RepositoriesEndPoint().RepositoriesResource(this.Workspace.slug);
            var repositories = repositoriesResource.ListRepositories();
            foreach (var repository in repositories)
            {
                var repositoryResource = repositoriesResource.RepositoryResource(repository.slug);
                repositoryResource.DeleteRepository();
                Console.WriteLine($"Repository {this.Workspace.slug}/{repository.slug} has been deleted");
            }
        }

        private void Switch(string[] args)
        {
            if (args.Length != 2)
            {
                Console.Error.WriteLine("Invalid command arguments");
                return;
            }

            switch (args[0])
            {
                case "--workspace":
                    SwitchWorkspace(args[1]);
                    break;
                default:
                    Console.Error.WriteLine("Invalid command arguments");
                    break;
            }
        }

        private void SwitchWorkspace(string workspaceSlugOrUuid)
        {
            this.Workspace = SharpBucket.WorkspacesEndPoint().WorkspaceResource(workspaceSlugOrUuid).GetWorkspace();
        }

        private void List(string[] args)
        {
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Invalid command arguments");
                return;
            }

            switch (args[0])
            {
                case "--repositories":
                    ListRepositories();
                    break;
                case "--workspaces":
                    ListWorkspaces();
                    break;
                default:
                    Console.Error.WriteLine("Invalid command arguments");
                    break;
            }
        }

        private void ListRepositories()
        {
            var repositoriesResource = this.SharpBucket.RepositoriesEndPoint().RepositoriesResource(this.Workspace.slug);
            var repositories = repositoriesResource.EnumerateRepositories();
            foreach (var repository in repositories)
            {
                Console.WriteLine(repository.slug);
                Console.WriteLine("  name: " + repository.name);
                Console.WriteLine("  uuid: " + repository.uuid);
                Console.WriteLine("  is_private: " + repository.is_private);
            }
        }

        private void ListWorkspaces()
        {
            var workspaces = SharpBucket.WorkspacesEndPoint().EnumerateWorkspaces();
            foreach (var workspace in workspaces)
            {
                Console.WriteLine(workspace.slug);
                Console.WriteLine("  name: " + workspace.name);
                Console.WriteLine("  uuid: " + workspace.uuid);
                Console.WriteLine("  is_private: " + workspace.is_private);
            }
        }
    }
}
