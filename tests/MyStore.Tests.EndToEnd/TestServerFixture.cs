using System;
using System.IO;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.PlatformAbstractions;
using MyStore.Web;
using Newtonsoft.Json;

namespace MyStore.Tests.EndToEnd
{
    public class TestServerFixture : IDisposable
    {
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var testsPath = PlatformServices.Default.Application.ApplicationBasePath;
            var webPath = "../../../../../src/MyStore.Web";
            var appPath = Path.GetFullPath(Path.Combine(testsPath, webPath));

            var server = new TestServer(WebHost.CreateDefaultBuilder()
                .UseEnvironment("test")
                .UseContentRoot(appPath)
                .UseStartup<Startup>());

            Client = server.CreateClient();
        }

        public void Dispose()
        {
        }
        
        public StringContent GetContent(object value)
            => new StringContent(JsonConvert.SerializeObject(value), Encoding.UTF8, "application/json");
    }
}