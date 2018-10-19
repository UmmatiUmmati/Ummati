namespace Ummati.IntegrationTest
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class StaticFilesTest : IClassFixture<DefaultWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public StaticFilesTest(DefaultWebApplicationFactory<Startup> factory) =>
            this.client = factory.CreateClient();

        [Fact]
        public async Task GetFavicon_Default_ReturnsFaviconWith200Ok()
        {
            var response = await this.client.GetAsync("/favicon.ico");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("image/x-icon", response.Content.Headers.ContentType.MediaType);
            Assert.True(response.Headers.CacheControl.Public);
            Assert.Equal(TimeSpan.FromSeconds(31536000), response.Headers.CacheControl.MaxAge);
        }

        [Fact]
        public async Task GetManifest_Default_ReturnsManifestWith200Ok()
        {
            var response = await this.client.GetAsync("/manifest.json");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/json", response.Content.Headers.ContentType.MediaType);
            Assert.True(response.Headers.CacheControl.Public);
            Assert.Equal(TimeSpan.FromSeconds(31536000), response.Headers.CacheControl.MaxAge);
        }

        [Fact]
        public async Task GetRobotsTxt_Default_ReturnsRobotsTxtWith200Ok()
        {
            var response = await this.client.GetAsync("/robots.txt");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/plain", response.Content.Headers.ContentType.MediaType);
            Assert.True(response.Headers.CacheControl.Public);
            Assert.Equal(TimeSpan.FromSeconds(31536000), response.Headers.CacheControl.MaxAge);
        }

        [Fact]
        public async Task GetServiceWorker_Default_ReturnsServiceWorkerWith200Ok()
        {
            var response = await this.client.GetAsync("/service-worker.js");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("application/javascript", response.Content.Headers.ContentType.MediaType);
            Assert.True(response.Headers.CacheControl.NoCache);
            Assert.True(response.Headers.CacheControl.NoStore);
        }
    }
}
