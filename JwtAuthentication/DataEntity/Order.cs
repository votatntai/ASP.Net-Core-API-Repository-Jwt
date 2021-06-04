using System;
using System.Collections.Generic;

namespace JwtAuthentication.DataEntity
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Status { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
