using System;
using System.Text;
using pr1.Models;
using pr2.Service;

namespace pr1
{
    internal class Program
    {
        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8; // +укрмова сюдоо 
            ShopService shopService = new ShopService(); //створення сервісу

            //створення моделей
            Console.WriteLine("-----Наповнення магазину товарами-----");
            Phone smartphone = new Phone(1, "PocoLocoX8", 15000, "Android");
            Computer pc = new Computer(2, "KOMP", 60000, 15.3);
            Laptop laptop = new Laptop(3, "MacbookAir", 45000, true);
            Phone fakePhone = new Phone(1, "PocoLocoX8Copy", 5000, "Android");

            //наповнення сервісу моделями
            shopService.addProduct(smartphone);
            shopService.addProduct(pc);
            shopService.addProduct(laptop);
            shopService.addProduct(fakePhone);

            //створення клієнта
            Customer client = new Customer(123, "Влад", "klochcovlad73@gmail.com");
            
            //створення замовлення
            Console.WriteLine("\n-----Оформлення покупки-----");
            shopService.CreateOrder(777, client, [1, 3, 4]);
        }
    }
}