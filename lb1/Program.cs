using System;
using System.Collections.Generic;
using System.Text;
using pr1.exeptions;
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

            //чи є товари в замовленні - опа, німа ну і помилки тож німа
            shopService.PrintAllProducts();

            
            //генерація товарів
            Console.WriteLine("\n----- Наповнення магазину товарами -----");
            Phone smartphone = new Phone(IdGenerator.NextId(), "PocoLocoX8", 15000, "Android");
            Computer pc = new Computer(IdGenerator.NextId(), "Computer", 60000, 15.3);
            Laptop laptop = new Laptop(IdGenerator.NextId(), "MacbookAir", 45000, true);
            try
            {
                shopService.AddProduct(smartphone);
                shopService.AddProduct(pc);
                shopService.AddProduct(laptop);
            }
            catch (InvalidProductDataException ex)
            {
                Console.WriteLine($"ПАМИЛКА {ex.Message}");
            }

            //перевірка ваалідації - додавання без назви, з від'ємною ціною та вже існуючим айді 
            Phone checkProduct = new Phone(IdGenerator.NextId(), "", 10000, "Android");
            Phone checkProduct2 = new Phone(IdGenerator.NextId(), "Xiaomi 15G", -10, "Android");
            Phone checkProduct3 = new Phone(1, "POCOLOCO_DOPPLEGANGER", 12000, "Android");
            Console.WriteLine("\n");
            try
            {
                shopService.AddProduct(checkProduct);
            }
            catch (InvalidProductDataException ex){
                Console.WriteLine($"ПАМИЛКА {ex.Message}");
            }
            try
            {
                shopService.AddProduct(checkProduct2);
            }
            catch (InvalidProductDataException ex){
                Console.WriteLine($"ПАМИЛКА {ex.Message}");
            }
            try
            {
                shopService.AddProduct(checkProduct3);
            }
            catch (InvalidProductDataException ex){
                Console.WriteLine($"ПАМИЛКА {ex.Message}");
            }
            
            
            //чи є товари в замовленні - опа, є
            shopService.PrintAllProducts();

            
            //видалення товару
            Console.WriteLine("\n----- Видалення з репозиторію -----");
            shopService.TryDeleteProduct(2);
            shopService.PrintAllProducts();
            
            //+ покупець
            Console.WriteLine("\n----- Реєстрація покупця -----");
            Customer client = new Customer(1, "Влад", "klochcovlad73@gmail.com");
            Console.WriteLine($"Зареєстровано покупця: {client.Name} (ID: {client.Id}, Email: {client.Email})");

            
            
            //Оформлення замовлення
            Console.WriteLine("\n-----Оформлення 2-х покупок -----");
            //попитка взяти існуючі товари з айді 1,3  та неіснуючий 99
            List<int> cart = new List<int> { 1, 3, 99 };
            //замовлення creation
            Result<Order> orderResult = shopService.TryCreateOrder(client, cart);
            if (orderResult.IsSuccess && orderResult.Value != null)
            {
                Console.WriteLine($"Замовлення №{orderResult.Value.Id} сформовано - Сума: {orderResult.Value.TotalPrice} грн");
            }
            else
            {
                Console.WriteLine($"ПОМИЛКА ЗАМОВЛЕННЯ {orderResult.ErrorMessage}");
            }
            
            // оформлення повністю порожнього/помилкового замовлення
            Result<Order> emptyOrderResult = shopService.TryCreateOrder(client, new List<int> { 55, 77 });
            if (!emptyOrderResult.IsSuccess)
            {
                Console.WriteLine($"ПОМИЛКА ЗАМОВЛЕННЯ {emptyOrderResult.ErrorMessage}");
            }
           
            
            //звіт
            Console.WriteLine("\n----- Оформлення звіту -----");
            
            //експорт звіту
            shopService.GenerateReport("report.txt");

            //читання файлу
            ReportService.ReadAndVerifyReport("report.txt");

            // бробка помилок I/O
            Console.WriteLine("--- Демонстрація обробки помилових ситуацій I/O ---");
            
            // Спроба прочитати неіснуючий файл
            ReportService.ReadAndVerifyReport("67.txt");
            
            // Спроба зберегти файл без нормального шляху
            ReportService.ExportShopReport("Z:/666/666.txt", new Repository<Technologia>(), new Repository<Order>());

            Console.WriteLine("\n==================================================");
            Console.WriteLine("             РОБОТУ ПРОГРАМИ ЗАВЕРШЕНО             ");
            Console.WriteLine("==================================================");
        }
    }
}