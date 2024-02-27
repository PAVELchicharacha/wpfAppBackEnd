using Lab3;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labFrontEnd.model
{
    class PRICELIST
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
