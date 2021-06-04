using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JwtAuthentication.DataEntity;
using JwtAuthentication.Data;
using Microsoft.AspNetCore.Http;

namespace JwtAuthentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly AuthenticationShopContext _context;

        public OrdersController(AuthenticationShopContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderResponse>>> GetOrders([FromQuery] Pagination param)
        {
            return await _context.Orders.Where(x => x.Status.Contains("Unpaid") && x.OrderDetails.Count >= 1).Include(x => x.OrderDetails).Select(x => new OrderResponse
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

        // GET: api/Orders/5
        [HttpGet("ViewCart/{id}")]
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

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("CheckOut/{id}")]
        public async Task<IActionResult> PutOrderDetail(Guid id, OrderResponse order)
        {
            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("AddToCart")]
        public async Task<ActionResult<OrderResponse>> PostOrderDetail(OrderRequest orderRequest)
        {
            var user = _context.Users.Where(x => x.Id.Equals(orderRequest.UserId)).Include(x => x.Orders).FirstOrDefault();
            if (user.Orders.Count == 0)
            {
                var od = new Order();
                od.UserId = user.Id;
                od.Id = orderRequest.UserId;
                od.Status = "Unpaid";
                _context.Orders.Add(od);
            }
            await _context.SaveChangesAsync();

            return new OrderResponse();
        }

        private bool OrderDetailExists(Guid id)
        {
            return _context.OrderDetails.Any(e => e.OrderId == id);
        }
    }
}
