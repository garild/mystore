using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MyStore.Core.Domain;
using MyStore.Core.Domain.Repositories;
using MyStore.Web.Controllers;
using Xunit;

namespace MyStore.Tests.Controllers
{
    public class ProductsControllerTests
    {
        [Fact]
        public async Task get_should_return_products()
        {
            var fixture = new Fixture();
            var products = fixture.CreateMany<Product>();
            var productsRepositoryMock = new Mock<IProductRepository>();
            productsRepositoryMock.Setup(x => x.BrowseAsync(It.IsAny<string>()))
                .ReturnsAsync(products);
            
            var controller = new ProductsController(productsRepositoryMock.Object);

            var result = await controller.Get(new BrowseProducts());

            productsRepositoryMock.Verify(x => x.BrowseAsync(It.IsAny<string>()), Times.Once);
            result.Should().NotBeNull();

            var okResult = result.Should().BeOfType<OkObjectResult>();
            var expectedProducts = okResult.Subject.Value as IEnumerable<Product>;

            expectedProducts.Should().BeEquivalentTo(products);
        }
    }
}