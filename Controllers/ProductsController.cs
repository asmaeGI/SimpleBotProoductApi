﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BotWebApi.Model;
using BotWebApi.ModelState;

namespace BotWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet("/api/Products")]
        public IEnumerable<Product> GetProducts()
        {
            return _context.Products;
        }

        //GET: api/Products/
        [HttpPost("/api/Products/ProductByName", Name ="ProductByName")]
        public async Task<List<Product>> ProductByName([FromBody] Product product)
        {
            return await _context.Products
                .Where(p => p.Name.Contains(product.Name))
                .ToListAsync();
        }
        
        //GET: api/Products/
        [HttpPost("/api/Products/ProductByCategorie", Name = "ProductByCategorie")]
        public async Task<List<Product>> ProductByCategorie([FromBody] ProductState product)
        {
            if (product.PriceMax == 0)
            {
                product.PriceMax = int.MaxValue;
            }
            return await _context.Products
                .Include(_=>_.Category)
                  .Where(p => p.Brand.Contains(product.Categorie) || p.Category.Name.Contains(product.Categorie) && p.Price > product.PriceMin && p.Price < product.PriceMax)
                 .ToListAsync();

        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}