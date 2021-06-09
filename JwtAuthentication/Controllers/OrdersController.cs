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
    public class OrdersController : ControllerBase
    {
        private readonly AuthenticationShopContext _context;

        public OrdersController(AuthenticationShopContext context)
        {
            _context = context;
        }

        [Authorize("Admin", "Saler")]
        [HttpGet]
        [Route("Orders")]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders([FromQuery] Pagination param)
        {
            return await _context.Orders.Include(x => x.OrderDetails).Select(x => new OrderResponse
            {
                Id = x.Id,
                UserId = x.UserId,
                Status = x.Status,
                CreateDate = x.CreateDate,
                OrderDetails = x.OrderDetails.Select(x => new OrderDetailsResponse
                {
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    Price = x.Price,
                    Quantity = x.Quantity,
                    CreateDate = x.CreateDate
                }).OrderBy(x => x.CreateDate).Skip((param.PageNumber - 1) * param.PageSize).Take(param.PageSize).ToArray()
            }).ToListAsync();
        }

        [Authorize("Saler", "User")]
        [HttpGet]
        [Route("Orders/{id}")]
        public async Task<ActionResult<OrderResponse>> GetOrderDetail(Guid id)
        {
            var orderDetail = await _context.Orders.Where(x => x.Id.Equals(id)).Include(x => x.OrderDetails).Select(x => new OrderResponse
            {
                Id = x.Id,
                Status = x.Status,
                UserId = x.UserId,
                CreateDate = x.CreateDate,
                OrderDetails = x.OrderDetails.Select(x => new OrderDetailsResponse
                {
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    CreateDate = x.CreateDate,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToArray()
            }).FirstOrDefaultAsync();

            if (orderDetail == null)
            {
                return NotFound();
            }

            return orderDetail;
        }

        [Authorize("User")]
        [HttpGet]
        [Route("Orders/MyOrders")]
        public async Task<ActionResult<ICollection<OrderResponse>>> GetMyOrders()
        {
            var user = (User) HttpContext.Items["User"];
            var myod = await _context.Orders.Where(x => x.UserId == user.Id).Include(x => x.OrderDetails).Select(x => new OrderResponse
            {
                Id = x.Id,
                Status = x.Status,
                UserId = x.UserId,
                CreateDate = x.CreateDate,
                OrderDetails = x.OrderDetails.Select(x => new OrderDetailsResponse
                {
                    OrderId = x.OrderId,
                    ProductId = x.ProductId,
                    CreateDate = x.CreateDate,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToArray()
            }).ToListAsync();

            return myod;
        }

        [Authorize("User")]
        [HttpPut]
        [Route("Orders/Update/{id}")]
        public async Task<IActionResult> PutOrder(OrderRequest odrq)
        {
            var user = (User) HttpContext.Items["User"];

            var id = _context.Orders.Where(x => x.UserId == user.Id).Select(x => x.Id).FirstOrDefault();

            var order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();

            var orderDetail = _context.OrderDetails.Where(x => x.OrderId == id);

            _context.OrderDetails.RemoveRange(orderDetail);

            order.OrderDetails = odrq.OrderDetails.Select(x => new OrderDetail
            {
                OrderId = id,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = (double)_context.Products.Where(a => a.Id == x.ProductId).Select(b => b.Price).FirstOrDefault() * x.Quantity,
            }).ToList();

            _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            return Content("Your order has been successfully updated");
        }

        [Authorize("Saler")]
        [HttpPut]
        [Route("Orders/Confirm/{id}")]
        public async Task<IActionResult> ConfirmOrder(Guid id)
        {
            var order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();

            if (order.Status == "Confirmed")
            {
                return Content("Previously confirmed order");
            }

            var orderd = _context.OrderDetails.Where(a => a.OrderId == id).ToList();

            List<Product> list = new List<Product>();

            foreach (var item in orderd)
            {
                var product = _context.Products.Where(x => x.Id == item.ProductId).FirstOrDefault();
                int orq = item.Quantity;
                product.Quantity = product.Quantity - orq;
                if (product.Quantity >= 0)
                {
                    list.Add(product);
                }
            }

            if (list.Count != orderd.Select(x => x.Product).Count())
            {
                return Content("The quantity in stock is not enough for this order");
            }

            _context.Products.UpdateRange(list);

            order.Status = "Confirmed";

            _context.Orders.Update(order);

            await _context.SaveChangesAsync();

            return Content("Confirmed");
        }

        [HttpPost]
        [Route("Orders/Checkout")]
        public async Task<ActionResult<OrderResponse>> PostOrderDetail(OrderRequest orderRequest)
        {
            var user = (User) HttpContext.Items["User"];

            var orderId = Guid.NewGuid();

            var order = new Order
            {
                Id = orderId,
                UserId = user.Id,
                Status = "Unconfirmed",
                OrderDetails = orderRequest.OrderDetails.Select(x => new OrderDetail
                {
                    OrderId = orderId,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    Price = (double)_context.Products.Where(a => a.Id == x.ProductId).Select(b => b.Price).FirstOrDefault() * x.Quantity
                }).ToList()
            };
            _context.Orders.Add(order);

            await _context.SaveChangesAsync();

            return new OrderResponse
            {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                CreateDate = order.CreateDate,
                OrderDetails = order.OrderDetails.Select(x => new OrderDetailsResponse
                {
                    OrderId = order.Id,
                    ProductId = x.ProductId,
                    Quantity = x.Quantity,
                    CreateDate = x.CreateDate,
                    Price = x.Price
                }).ToArray()
            };
        }

        [Authorize("User")]
        [HttpDelete]
        [Route("Orders/Delete/{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {

            var order = await _context.Orders.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return Content("The order has been successfully deleted");
        }
    }
}
