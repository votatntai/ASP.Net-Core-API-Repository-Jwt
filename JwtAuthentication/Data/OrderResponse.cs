using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
