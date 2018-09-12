using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyStore.Web.Domain;
using MyStore.Web.Models;

namespace MyStore.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private static readonly List<Product> _products = new List<Product>
        {
            new Product(Guid.NewGuid(), "Phones", "Samsung S8", 3000),
            new Product(Guid.NewGuid(), "Phones", "IPhone", 5000),
            new Product(Guid.NewGuid(), "Phones", "Xiaomi Mi6", 2000)
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
            _products.Add(new Product(id, request.Name, request.Category, request.Price));

            return CreatedAtAction(nameof(Get), new {id}, null);
        }
        
        
        //Views
        
        [HttpGet("view")]
        public IActionResult GetView()
        {
            var productsViewModel = _products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
            
            return View("Browse", productsViewModel);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            var viewModel = new CreateProductViewModel();
            
            return View(viewModel);
        }

        [HttpPost("create")]
        public IActionResult Create([FromForm] CreateProductViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }
            var id = Guid.NewGuid();
            _products.Add(new Product(id, viewModel.Name, viewModel.Category, viewModel.Price));

            return RedirectToAction(nameof(GetView));
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
        public string Category { get; set; }
    }
}