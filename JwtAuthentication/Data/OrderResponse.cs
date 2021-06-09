using System;

namespace JwtAuthentication.Data
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }
        public OrderDetailsResponse[] OrderDetails { get; set; }
    }
}
