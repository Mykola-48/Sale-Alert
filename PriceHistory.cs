using System;

namespace Course_Project_OOP_3
{
    public class PriceHistory
    {
        public int Id { get; set; }
        public decimal CurrentPrice { get; set; }
        public DateTime AddedDate { get; set; }


        // Forring Key
        public string ProductId { get; set; }
        // Link on the Product
        public Product Product { get; set; }
    }
}
