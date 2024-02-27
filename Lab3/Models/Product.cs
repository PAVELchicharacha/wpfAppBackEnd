using System;
using System.Collections.Generic;

namespace Lab3.Models;

public partial class Product
{
    public int Id { get; set; }

    public DateTime SaleDate { get; set; }

    public decimal ProductSales { get; set; }

    public int Quantity { get; set; }

    public decimal ProductCoast { get; set; }

    public int IdTovar { get; set; }

    public virtual PriceList IdTovarNavigation { get; set; } = null!;
}
