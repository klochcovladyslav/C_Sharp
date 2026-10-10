using System;
using System.Collections.Generic;
using System.Text;
using pr1.Base;
using pr1.Models;
using pr2.Service;

namespace pr1
{
    internal class Program
    {
        public static void Main()
        {
            //укрмова
            Console.OutputEncoding = Encoding.UTF8;

            
            Console.WriteLine("==================================================");
            Console.WriteLine("            СИСТЕМА ІНТЕРНЕТ-МАГАЗИНУ             ");
            Console.WriteLine("==================================================");

            
            //створення сервісу
            ShopService shopService = new ShopService();

            //чи є товари в замовленні - опа, німа
            shopService.PrintAllProducts();
            
            
            //генерація товарів
            Console.WriteLine("\n----- Наповнення магазину товарами -----");
            Phone smartphone = new Phone(IdGenerator.NextId(), "PocoLocoX8", 15000, "Android");
            Computer pc = new Computer(IdGenerator.NextId(), "Computer", 60000, 15.3);
            Laptop laptop = new Laptop(IdGenerator.NextId(), "MacbookAir", 45000, true);
            shopService.AddProduct(smartphone);
            shopService.AddProduct(pc);
            shopService.AddProduct(laptop);
            
            
            //перевірка ваалідації - додавання без назви, з від'ємною ціною та вже існуючим айді 
            Phone checkProduct = new Phone(IdGenerator.NextId(), "", 10000, "Android");
            Phone checkProduct2 = new Phone(IdGenerator.NextId(), "Xiaomi 15G", -10, "Android");
            Phone checkProduct3 = new Phone(1, "POCOLOCO_DOPPLEGANGER", 12000, "Android");
            Console.WriteLine("\n");
            shopService.AddProduct(checkProduct);   
            shopService.AddProduct(checkProduct2); 
            shopService.AddProduct(checkProduct3); 
            
            //чи є товари в замовленні - опа, є
            shopService.PrintAllProducts();

            
            //видалення товару
            Console.WriteLine("\n----- Видалення з репозиторію -----");
            shopService.DeleteProduct(2);
            shopService.PrintAllProducts();
            
            //+ покупець
            Console.WriteLine("\n----- Реєстрація покупця -----");
            Customer client = new Customer(1, "Влад", "klochcovlad73@gmail.com");
            Console.WriteLine($"Зареєстровано покупця: {client.Name} (ID: {client.Id}, Email: {client.Email})");

            
            
            //Оформлення замовлення
            Console.WriteLine("\n----- Оформлення покупки -----");
            //попитка взяти існуючі товари з айді 1,3  та неіснуючий 99
            List<int> cart = new List<int> { 1, 3, 99 };
            //замовлення creation
            shopService.CreateOrder(client, cart);

            

            Console.WriteLine("\n==================================================");
            Console.WriteLine("             РОБОТУ ПРОГРАМИ ЗАВЕРШЕНО             ");
            Console.WriteLine("==================================================");
        }
    }
}