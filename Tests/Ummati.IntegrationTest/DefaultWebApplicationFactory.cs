namespace Ummati.IntegrationTest
{
    using System;
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
            var directory = GetDirectoryPath(new DirectoryInfo(Directory.GetCurrentDirectory()));
            builder.UseWebRoot(Path.Combine(directory.FullName, "Client", "dist"));
        }

        private static DirectoryInfo GetDirectoryPath(DirectoryInfo directory)
        {
            var file = directory
                .GetFiles("*", SearchOption.AllDirectories)
                .Where(x => string.Equals(Path.GetFileName(x.FullName), "Ummati.csproj", StringComparison.OrdinalIgnoreCase))
                .FirstOrDefault();
            if (file == null)
            {
                return GetDirectoryPath(directory.Parent);
            }

            return file.Directory;
        }
    }
}
