using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using MyStore.Web;
using Xunit;

namespace MyStore.Tests.EndToEnd.Controllers
{
    public class HomeControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;

        public HomeControllerTests(TestServerFixture fixture)
        {
            _client = fixture.Client;
        }
        
        [Fact]
        public async Task get_index_should_return_view()
        {
            var response = await _client.GetAsync("/");

            response.IsSuccessStatusCode.Should().BeTrue();

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotBeEmpty();
        }
    }
}