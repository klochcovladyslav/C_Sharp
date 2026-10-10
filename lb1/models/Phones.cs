using System;
namespace pr1.Models;


public class Phone : Technologia
{
    public string Os { get; set; }

    public Phone(int id, string name, decimal price, string operatingSystem) : base(id, name, price)
    {
        Os = operatingSystem;
    }

    public override void GetDescription()
    {
        Console.WriteLine($"ID: {Id}, Телефон: {Name}, ОС: {Os}, Ціна: {Price} грн");
    }
}


