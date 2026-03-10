using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // for auto incrementing columns

namespace Course_Project_OOP_3
{
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Telling that this column is auto increment
        public int RecordNum { get; set; }

        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        

        // У одного товару може бути багато записів ціни
        public ICollection<PriceHistory> Prices { get; set; } = new List<PriceHistory>();
    }
}
