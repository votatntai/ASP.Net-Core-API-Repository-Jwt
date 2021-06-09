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

        [HttpGet]
        [Route("Products")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProducts([FromQuery] Pagination param)
        {
            var products = await _context.Products.Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToListAsync(); ;
            return Ok(new ResponsePagination<ProductResponse>(products)
            {
                Total = _context.Products.Count(),
                Type = "Products"
            });
        }

        [HttpGet]
        [Route("Products/{id}")]
        public async Task<ActionResult<ProductResponse>> GetProduct(Guid id)
        {
            var product = await _context.Products.Where(x => x.Id == id).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
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

        [HttpGet]
        [Route("Products/Name/{value}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByName(string value, [FromQuery] Pagination param)
        {
            var products = await _context.Products.Where(x => x.Name.Contains(value)).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).ToListAsync();
            var total = products.Count();
            var result = products.OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToList();
            return Ok(new ResponsePagination<ProductResponse>(result)
            {
                Total = total,
                Type = "Products"
            });
        }

        [HttpGet]
        [Route("Products/Maker/{value}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByMaker(string value, [FromQuery] Pagination param)
        {
            var products = await _context.Products.Where(x => x.Maker.Contains(value)).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).ToListAsync();
            var total = products.Count();
            var result = products.OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToList();
            return Ok(new ResponsePagination<ProductResponse>(result)
            {
                Total = total,
                Type = "Products"
            });
        }

        [HttpGet]
        [Route("Products/Price/{min}/{max}")]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByPrice(decimal min, decimal max, [FromQuery] Pagination param)
        {
            var products = await _context.Products.Where(x => x.Price > min && x.Price < max).Select(x => new ProductResponse
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Maker = x.Maker,
                Category = x.Category,
                MinQuantity = x.MinQuantity,
                Price = x.Price,
                Quantity = x.Quantity,
                ImageUrl = x.ImageUrl,
                CreateDate = x.CreateDate
            }).ToListAsync();
            var total = products.Count();
            var result = products.OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToList();
            return Ok(new ResponsePagination<ProductResponse>(result)
            {
                Total = total,
                Type = "Products"
            });
        }

        [Authorize("Modifier")]
        [HttpPost]
        [Route("Products/Add")]
        public async Task<ActionResult<ProductResponse>> PostProduct(ProductRequest pror)
        {
            var id = Guid.NewGuid();
            var product = new Product
            {
                Id = id,
                Name = pror.Name,
                Description = pror.Description,
                Maker = pror.Maker,
                Category = pror.Category,
                Price = pror.Price,
                Quantity = pror.Quantity,
                MinQuantity = pror.MinQuantity,
                ImageUrl = pror.ImageUrl
            };
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return new ProductResponse
            {
                Id = id,
                Name = pror.Name,
                Description = pror.Description,
                Maker = pror.Maker,
                Category = pror.Category,
                Price = pror.Price,
                Quantity = pror.Quantity,
                MinQuantity = pror.MinQuantity,
                ImageUrl = pror.ImageUrl
            };
        }

        [Authorize("Modifier")]
        [HttpPut]
        [Route("Products/Update")]
        public async Task<IActionResult> PutProduct(Guid id, ProductRequest pror)
        {
            if (!ProductExists(id))
            {
                return NotFound();
            }

            var product = _context.Products.Where(x => x.Id == id).Select(a => new Product
            {
                Name = pror.Name,
                Description = pror.Description,
                Maker = pror.Maker,
                Category = pror.Category,
                Price = pror.Price,
                Quantity = pror.Quantity,
                MinQuantity = pror.MinQuantity,
                ImageUrl = pror.ImageUrl,
            }).FirstOrDefault();

            _context.Update(product);

            await _context.SaveChangesAsync();

            return Content("The product has been successfully updated");
        }

        [Authorize("Modifier")]
        [HttpDelete]
        [Route("Products/Delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            var piord = _context.OrderDetails.Where(x => x.ProductId == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            if (piord != null)
            {
                return Content("This product has in the orders, can not remove");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Content("The product has been successfully deleted");
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
