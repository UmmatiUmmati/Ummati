namespace Ummati.IntegrationTest.Controllers
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Xunit;

    public class HomeControllerTest : IClassFixture<DefaultWebApplicationFactory<Startup>>
    {
        private readonly HttpClient client;

        public HomeControllerTest(DefaultWebApplicationFactory<Startup> factory) =>
            this.client = factory.CreateClient();

        [Fact]
        public async Task GetIndex_Root_ReturnsHtmlWith200Ok()
        {
            var response = await this.client.GetAsync("/");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers.ContentType.MediaType);
            Assert.True(response.Headers.CacheControl.Public);
            Assert.Equal(TimeSpan.FromSeconds(31536000), response.Headers.CacheControl.MaxAge);
        }

        [Fact]
        public async Task GetIndex_NotRoot_ReturnsHtmlWith200Ok()
        {
            var response = await this.client.GetAsync("/not-found");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("text/html", response.Content.Headers.ContentType.MediaType);
        }
    }
}
