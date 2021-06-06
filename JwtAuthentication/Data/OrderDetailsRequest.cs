using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtAuthentication.Data
{
    public class OrderDetailsRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
