using System.Collections.Generic;
using pr1.Base;

namespace pr1.Models;

public class Order : Entity
{
    public Customer Buyer { get; set; }
    public List<Technologia> Products { get; set; } = new();
    public decimal TotalPrice { get; set; }

    public Order(int id, Customer buyer) : base(id)
    {
        Buyer = buyer;
    }
}