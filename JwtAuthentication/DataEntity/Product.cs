using System;
using System.Collections.Generic;

namespace JwtAuthentication.DataEntity
{
    public partial class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MinQuantity { get; set; }
        public DateTime CreateDate { get; set; }

        public virtual OrderDetail OrderDetail { get; set; }
    }
}
