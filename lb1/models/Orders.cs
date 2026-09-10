using System.Collections.Generic;
using pr1.Base;

namespace pr1.Models;

public class Order
{
    public int OrderId { get; set; }        
    public Customer Buyer { get; set; }
    public List<Technologia> Products { get; set; } = new List<Technologia>();
    public decimal TotalPrice { get; set; }
}