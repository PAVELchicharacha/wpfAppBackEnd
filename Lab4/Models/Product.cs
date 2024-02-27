using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime? SaleDate { get; set; }
        public double? ProductSales { get; set; }// кол-во проданного
        public double? Quantity { get; set; }//кол-во товара
        [Required]
        public double ProductCoast { get; set; }//цена продукта
        public Pricelist? PriceLists { get; set; }
    }
}
