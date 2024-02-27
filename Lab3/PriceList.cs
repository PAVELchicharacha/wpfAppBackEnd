using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab3
{
    public class PriceList
    {
        [Required]
        public int Id { get; set; }
        [Key]
        public string? Name { get; set; }
        [Required]
        public double? Coast { get; set; }
        [Required]
        List<Product> ProductLists { get; set; } = new();
    }
}
