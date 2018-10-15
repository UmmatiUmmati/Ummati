#addin "Cake.Docker"

using System.Collections.Generic;

var target = Argument("Target", "Default");
var version =
    HasArgument("Version") ? Argument<string>("Version") :
    TFBuild.IsRunningOnVSTS ? TFBuild.Environment.Build.Number :
    EnvironmentVariable("Version") != null ? EnvironmentVariable("Version") :
    "1.0.0";
var branch =
    HasArgument("Branch") ? Argument<string>("Branch") :
    TFBuild.IsRunningOnVSTS ? TFBuild.Environment.Repository.Branch :
    EnvironmentVariable("Branch") != null ? EnvironmentVariable("Branch") :
    "master";
var dockerImageName =
    HasArgument("DockerImageName") ? Argument<string>("DockerImageName") :
    EnvironmentVariable("DockerImageName") != null ? EnvironmentVariable("DockerImageName") :
    "ummati";
var dockerServer =
    HasArgument("DockerServer") ? Argument<string>("DockerServer") :
    EnvironmentVariable("DockerServer") != null ? EnvironmentVariable("DockerServer") :
    null;
var dockerUsername =
    HasArgument("DockerUsername") ? Argument<string>("DockerUsername") :
    EnvironmentVariable("DockerUsername") != null ? EnvironmentVariable("DockerUsername") :
    null;
var dockerPassword =
    HasArgument("DockerPassword") ? Argument<string>("DockerPassword") :
    EnvironmentVariable("DockerPassword") != null ? EnvironmentVariable("DockerPassword") :
    null;

var artifactsDirectory = Directory("./Artifacts");

Task("Clean")
    .Does(() =>
    {
        CleanDirectory(artifactsDirectory);
        DeleteDirectories(GetDirectories("**/bin"), new DeleteDirectorySettings() { Force = true, Recursive = true });
        DeleteDirectories(GetDirectories("**/obj"), new DeleteDirectorySettings() { Force = true, Recursive = true });
        DeleteDirectories(GetDirectories("**/Ummati/Client/dist"), new DeleteDirectorySettings() { Force = true, Recursive = true });
    });

Task("Restore")
    .IsDependentOn("Clean")
    .Does(() =>
    {
        DotNetCoreRestore();
    });

 Task("Build")
    .IsDependentOn("Restore")
    .Does(() =>
    {
        foreach(var project in GetFiles("./**/*.csproj"))
        {
            DotNetCoreBuild(
                project.GetDirectory().FullPath,
                new DotNetCoreBuildSettings()
                {
                    Configuration = "Release",
                    MSBuildSettings = new DotNetCoreMSBuildSettings().SetVersion(version),
                    NoRestore = true
                });
        }
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(() =>
    {
        foreach(var project in GetFiles("./Tests/**/*.csproj"))
        {
            DotNetCoreTest(
                project.ToString(),
                new DotNetCoreTestSettings()
                {
                    Configuration = "Release",
                    Logger = $"trx;LogFileName={project.GetFilenameWithoutExtension()}.trx",
                    NoBuild = true,
                    NoRestore = true,
                    ResultsDirectory = artifactsDirectory
                });
        }
    });

Task("DockerBuild")
    .Does(() =>
    {
        var labels = new string[]
        {
            $"org.opencontainers.image.created='{DateTimeOffset.UtcNow:o}'",
            $"org.opencontainers.image.authors='https://github.com/orgs/Ummati-com/people'",
            $"org.opencontainers.image.url='https://github.com/Ummati-com/Ummati'",
            $"org.opencontainers.image.documentation='https://github.com/Ummati-com/Ummati'",
            $"org.opencontainers.image.source='https://github.com/Ummati-com/Ummati'",
            $"org.opencontainers.image.version='{version}'",
            $"org.opencontainers.image.vendor='Ummati'",
            $"org.opencontainers.image.title='Ummati'",
            $"org.opencontainers.image.description='TODO'"
        };
        foreach(var dockerfile in GetFiles("./**/Dockerfile"))
        {
            DockerBuild(
                new DockerImageBuildSettings()
                {
                    File = dockerfile.ToString(),
                    Label = labels,
                    Tag = GetDockerTags(dockerfile)
                },
                ".");
        }
    });

Task("DockerLogin")
    .Does(() =>
    {
        DockerLogin(dockerUsername, dockerPassword, dockerServer);
    });

Task("DockerPush")
    .IsDependentOn("DockerLogin")
    .Does(() =>
    {
        foreach(var dockerfile in GetFiles("./**/Dockerfile"))
        {
            foreach (var dockerImageTag in GetDockerTags(dockerfile))
            {
                try
                {
                    DockerPush(dockerImageTag);
                }
                catch (CakeException exception)
                {
                    if (exception.Message.Contains("Process returned an error (exit code 1)."))
                    {
                        Warning($"An image with the same tag '{dockerImageTag}' already exists.");
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
    });

Task("DockerClean")
    .Does(() =>
    {
        DockerRmi(dockerImageName);
    });

Task("Default")
    .IsDependentOn("Test");

public string[] GetDockerTags(FilePath dockerfileFilePath)
{
    return new string[]
    {
        GetDockerTag(dockerServer, dockerImageName, branch),
        GetDockerTag(dockerServer, dockerImageName, branch, version)
    };
}

public static string GetDockerTag(string dockerServer, string name, string branch = "master", string version = "latest")
{
    var tag = $"{name}:{version}";

    if (!string.IsNullOrEmpty(dockerServer))
    {
        tag = $"{dockerServer.TrimEnd('/')}/{tag}";
    }

    if (!string.Equals(branch, "master", StringComparison.Ordinal))
    {
        tag = $"{tag}-{branch}";
    }

    return tag;
}

RunTarget(target);