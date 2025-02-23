﻿namespace SiteV4.Models
{
    public class Cart
    {
        public int CartID { get; set; }
        public int UserID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public User User { get; set; } = null!;
        public Product Product { get; set; } = null!;
    }
}
