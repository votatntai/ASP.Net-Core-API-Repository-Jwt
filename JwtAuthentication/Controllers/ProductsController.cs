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
        [Route("Products")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts([FromQuery] Pagination param)
        {
            return await _context.Products.Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // GET: api/Products/5
        [HttpGet]
        [Route("Products/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _context.Products.Where(x => x.Id == id).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // GET: api/Products/Name/abc
        [HttpGet]
        [Route("Products/Name/{value}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByName(string value, [FromQuery] Pagination param)
        {
            return await _context.Products.Where(x => x.Name.Equals(value)).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // GET: api/Products/Maker/abc
        [HttpGet]
        [Route("Products/Maker/{value}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByMaker(string value, [FromQuery] Pagination param)
        {
            return await _context.Products.Where(x => x.Maker.Equals(value)).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // GET: api/Products/Price/
        [HttpGet]
        [Route("Products/Price/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByPrice(decimal min, decimal max, [FromQuery] Pagination param)
        {
            return await _context.Products.Where(x => x.Price > min && x.Price < max).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Products/Add")]
        public async Task<ActionResult<ProductResponse>> PostProduct(ProductRequest pror)
        {
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = pror.Name,
                Maker = pror.Maker,
                Category = pror.Category,
                Price = pror.Price,
                Quantity = pror.Quantity,
                MinQuantity = pror.MinQuantity,
                ImageUrl = pror.ImageUrl
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductResponse {
                Id = id,
                Name = pror.Name,
                Maker = pror.Maker,
                Category = pror.Category,
                Price = pror.Price,
                Quantity = pror.Quantity,
                MinQuantity = pror.MinQuantity,
                ImageUrl = pror.ImageUrl
            };
        }

        // PUT: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Products/Update")]
        public async Task<IActionResult> PutProduct(Guid id, ProductRequest pror)
        {

            var product = new Product
            {
                Id = id,
                Name = pror.Name,
                Maker = pror.Maker,
                Category = pror.Category,
                Price = pror.Price,
                Quantity = pror.Quantity,
                MinQuantity = pror.MinQuantity,
                ImageUrl = pror.ImageUrl,
                CreateDate = DateTime.Now
            };

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // DELETE: api/Products/5
        [HttpDelete]
        [Route("Products/Delete/{id}")]
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
