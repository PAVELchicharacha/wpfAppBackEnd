using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
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
        public int PriceListId { get; set; }    
        public PriceList? PriceList { get; set; }
    }
}
