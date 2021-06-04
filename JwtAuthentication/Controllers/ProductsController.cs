using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtAuthentication.DataEntity;
using JwtAuthentication.Data;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AuthenticationShopContext _context;

        public ProductsController(AuthenticationShopContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts([FromQuery] Pagination param)
        {
            return await _context.Products.Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _context.Products.Where(x => x.Id == id).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateDate = x.CreateDate
            }).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/Name/abc
        [HttpGet("name/{value}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByName(string value, [FromQuery] Pagination param)
        {
            return await _context.Products.Where(x => x.Name.Equals(value)).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // GET: api/Products/Maker/abc
        [HttpGet("maker/{value}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByMaker(string value, [FromQuery] Pagination param)
        {
            return await _context.Products.Where(x => x.Maker.Equals(value)).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // GET: api/Products/Price/
        [HttpGet("price/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByPrice(decimal min, decimal max, [FromQuery] Pagination param)
        {
            return await _context.Products.Where(x => x.Price > min && x.Price < max).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
