using System;

namespace JwtAuthentication.Data
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Maker { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int MinQuantity { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
