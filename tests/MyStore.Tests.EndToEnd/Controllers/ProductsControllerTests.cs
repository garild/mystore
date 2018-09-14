using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using MyStore.Core.Domain;
using MyStore.Web.Controllers;
using Newtonsoft.Json;
using Xunit;

namespace MyStore.Tests.EndToEnd.Controllers
{
    public class ProductsControllerTests : IClassFixture<TestServerFixture>
    {
        private readonly HttpClient _client;

        public ProductsControllerTests(TestServerFixture fixture)
        {
            _client = fixture.Client;
        }
        
        [Fact]
        public async Task get_should_return_products()
        {
            var response = await _client.GetAsync("products");

            response.IsSuccessStatusCode.Should().BeTrue();

            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(content);

            products.Should().NotBeEmpty();
            products.Count().Should().Be(1);
        }
        
        [Fact]
        public async Task post_should_create_product()
        {
            var json = new CreateProduct
            {
                Name = "test 1",
                Price = 123,
                CategoryId = Guid.Parse("662b4780-b5bc-40af-8963-91c2a4da28d8")
            };
            
            var response = await _client.PostAsJsonAsync("products", json);

            response.IsSuccessStatusCode.Should().BeTrue();
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            response.Headers.Location.Should().NotBeNull();
        }       
    }
}