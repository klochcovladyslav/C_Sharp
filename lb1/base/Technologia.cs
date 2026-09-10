using System;
namespace pr1.Base;

public class Technologia
{
    public int Id{get;protected set;}
    private string _name;
    private decimal _price;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
    public decimal Price{
        get { return _price; }
        set
        {
            if (value < 0) _price = 0;
            else _price = value;
        }
    }

    public Technologia(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }

    public virtual void GetDescription()
    {
        Console.WriteLine($"ID: {Id}, Назва: {Name}, Ціна: {Price}");
    }
}