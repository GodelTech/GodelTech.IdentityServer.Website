using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace GodelTech.IdentityServer.Website.IntegrationTests
{
    public class HelloWorldTests : IClassFixture<WebApplicationFactoryFixture>
    {
        readonly HttpClient _client;

        public HelloWorldTests(WebApplicationFactoryFixture fixture)
        {
            _client = fixture.CreateClient();
        }

        [Fact]
        public async Task DiscoveryEndpoint_ReturnJson()
        {
            // Arrange & Act
            var response = await _client.GetAsync("/");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal("Hello World!", await response.Content.ReadAsStringAsync());
        }
    }
}