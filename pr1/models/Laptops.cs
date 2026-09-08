using System;
using pr1.Base;

namespace pr1.Models;


public class Laptop : Technologia
{
    public bool Transportable { get; set; }

    public Laptop(int id, string name, decimal price, bool transportable) : base(id, name, price)
    {
        Transportable = transportable;
    }

    public override void GetDescription()
    {
        Console.WriteLine($"ID: {Id}, Назва: {Name}, Переносимий: {Transportable}, Ціна: {Price}");
    }
}





