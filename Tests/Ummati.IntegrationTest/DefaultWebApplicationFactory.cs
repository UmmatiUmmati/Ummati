namespace Ummati.IntegrationTest
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc.Testing;

    public class DefaultWebApplicationFactory<TEntryPoint> : WebApplicationFactory<TEntryPoint>
        where TEntryPoint : class
    {
        protected override void ConfigureClient(HttpClient client) =>
            client.BaseAddress = new Uri("http://localhost");

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            base.ConfigureWebHost(builder);
            var projectDirectoryPath = GetProjectDirectory(new DirectoryInfo(Directory.GetCurrentDirectory())).FullName;
            var distDirectoryPath = Path.Combine(projectDirectoryPath, "Client", "dist");
            builder.UseWebRoot(distDirectoryPath);
        }

        private static DirectoryInfo GetProjectDirectory(DirectoryInfo directory)
        {
            var file = directory
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(x => string.Equals(Path.GetFileName(x.FullName), "Ummati.csproj", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (file == null)
            {
                return GetProjectDirectory(directory.Parent);
            }

            return file.Directory;
        }
    }
}
