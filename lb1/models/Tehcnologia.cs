using System;
using pr1.Base;

namespace pr1.Models;

public abstract class Technologia : Entity
{
    public string Name { get; set; }
    public decimal Price { get; set; }

    protected Technologia(int id, string name, decimal price) : base(id)
    {
        Name = name;
        if (price < 0)
        {
            Price = 0;
        }
        else
        {
            Price = price;
        }
    }

    public virtual void GetDescription()
    {
        Console.WriteLine($"ID: {Id}, Назва: {Name}, Ціна: {Price} грн");
    }
}