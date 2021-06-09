using System.Collections.Generic;

namespace JwtAuthentication.Data
{
    public class OrderRequest
    {
        public ICollection<OrderDetailsRequest> OrderDetails { get; set; }
    }
}
