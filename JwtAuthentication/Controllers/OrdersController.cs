﻿using System;
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

        // GET: api/Orders/5
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

        // PUT: api/Orders/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        [Route("Orders/Update/{id}")]
        public async Task<IActionResult> PutOrder(Guid id, OrderRequest odrq)
        {
            var user = _context.Users.Where(x => x.Id.Equals(odrq.UserId)).Include(x => x.Orders).FirstOrDefault();

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

            return NoContent();
        }

        // POST: api/Orders
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Orders/Checkout")]
        public async Task<ActionResult<OrderResponse>> PostOrderDetail(OrderRequest orderRequest)
        {
            var user = _context.Users.Where(x => x.Id.Equals(orderRequest.UserId)).Include(x => x.Orders).FirstOrDefault();

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

        private bool OrderExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
