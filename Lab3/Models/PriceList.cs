using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Lab3.Models;

public partial class PriceList
{
    public string Name { get; set; } = null!;

    public int Id { get; set; }

    public double Coast { get; set; }
    [JsonIgnore]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
