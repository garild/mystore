using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Domain;

namespace MyStore.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private static readonly List<Product> _products = new List<Product>
        {
            new Product(Guid.NewGuid(), "Samsung S8", 3000),
            new Product(Guid.NewGuid(), "IPhone", 5000),
            new Product(Guid.NewGuid(), "Xiaomi Mi6", 2000)
        };

        [HttpGet]
        public IActionResult Get([FromQuery] BrowseProducts query)
        {
            var products = _products;
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name)).ToList();
            }
            // more filters

            return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            var product = _products.SingleOrDefault(p => p.Id == id);
            if (product != null)
            {
                return Ok(product);
            }

            return NotFound();
        }

        [HttpPost]
        public IActionResult Post(CreateProduct request)
        {
            var id = Guid.NewGuid();
            _products.Add(new Product(id, request.Name, request.Price));

            return CreatedAtAction(nameof(Get), new {id}, null);
        }
    }

    public class BrowseProducts
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class CreateProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}