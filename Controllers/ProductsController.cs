using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ServerApp.Models;
using ServerApp.Data;
using ServerApp.DTO;
using Microsoft.EntityFrameworkCore;

namespace ServerApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController:ControllerBase
    {
        private readonly SocialContext _context;
        public ProductsController(SocialContext context)
        {
            _context = context;
        }
    

        [HttpGet]
        public async Task<ActionResult> GetProducts(){
            var products = await _context
            .Products
            .Select(p=> ProductToDTO(p))
            .ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id){
            var p = await _context.Products.FindAsync(id);
            if(p==null)
              return NotFound();
            return Ok(ProductToDTO(p));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Product entity){
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduct), new{id=entity.Id}, ProductToDTO(entity));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product p){
            if(id!=p.Id){
                return BadRequest();
            }
            var product = await _context.Products.FindAsync(id);

            if(product == null){
                return NotFound();
            }

            product.Name = p.Name;
            product.Price = p.Price;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id){
            var product = await _context.Products.FindAsync(id);
            if(product==null){
                return NotFound();
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private static ProductDTO ProductToDTO(Product p){
            return new ProductDTO(){
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                IsActive = p.IsActive
            };
        }
    }
}