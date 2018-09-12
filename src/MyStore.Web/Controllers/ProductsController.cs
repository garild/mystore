using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get() => Ok(_products);

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

    public class CreateProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        
        public Product(Guid id, string name, decimal price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
    }
}