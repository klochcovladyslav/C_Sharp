using System;
namespace pr1.Models;


public class Computer : Technologia
{
    public double ScreenSize { get; set; }

    public Computer(int id, string name, decimal price, double size) : base(id, name, price)
    {
        ScreenSize = size;
    }

    public override void GetDescription()
    {
        Console.WriteLine($"ID: {Id}, Назва: {Name}, Розмір екрану: {ScreenSize} дюйма, Ціна: {Price}");
    }
}



