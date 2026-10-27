using System;
using System.IO;
using pr1.Base;
using pr1.Models;

namespace pr2.Service;

public class ReportService
{
    //Формування та експорт звіту у файлік
    public static void ExportShopReport(string filePath, Repository<Technologia> productRepo, Repository<Order> orderRepo)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
            {
                writer.WriteLine("==================================================");
                writer.WriteLine("        ЗВІТ ПРО СТАН СИСТЕМИ МАЗАЗИНУ            ");
                writer.WriteLine($"        Дата формування: {DateTime.Now}          ");
                writer.WriteLine("==================================================\n");
                
                
                var products = productRepo.GetAll();
                writer.WriteLine("------СКЛАД------");
                writer.WriteLine($"Загальна кількість товарів на складі: {products.Count}");
                
                var orders = orderRepo.GetAll();
                writer.WriteLine($"Загальна кількість створених замовлень: {orders.Count}\n");

                writer.WriteLine("---ПЕРЕЛІК ТОВАРІВ---");
                if (products.Count == 0)
                {
                    writer.WriteLine("Товари відсутні.");
                }
                else
                {
                    foreach (var p in products)
                    {
                        writer.WriteLine($"ID: {p.Id}, Назва: {p.Name}, Ціна: {p.Price} грн, Тип: {p.GetType().Name}");
                    }
                }

                writer.WriteLine("\n---СТАТИСТИКА---");
                decimal total = 0;
                foreach (var o in orders)
                {
                    total += o.TotalPrice;
                    writer.WriteLine($"Замовлення N{o.Id}, Покупець: {o.Buyer.Name}, Позицій: {o.Products.Count}, Сума: {o.TotalPrice} грн");
                }
                writer.WriteLine($"\n Скуплено товарів на: {total} грн");
                writer.WriteLine("==================================================");
            }

            Console.WriteLine($"УСПІХ - Звіт експортовано у файл: {filePath}");
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine($"ВІДКАЗАНО В ДОСТУПІ - Немає прав для запису у файл '{filePath}': {ex.Message}");
        }
        catch (DirectoryNotFoundException ex)
        {
            Console.WriteLine($"НЕПРАВИЛЬНИЙ ШЛЯХ - Вказаний файл не знайдено: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"ПОМИЛКА I/O - Помилка введення/виведення при записі у файл: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ПРОСТО ЯКАСЬ НЕПЕРЕДБАЧУВАНА ПОМИЛКА {ex.Message}");
        }
    }

    //Зчитування та перевірка коректності сформованого файлу
    public static void ReadAndVerifyReport(string filePath)
    {
        Console.WriteLine($"\n--- Читання та перевірка файлу звіту ({filePath}) ---");
        try
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Файл за шляхом '{filePath}' не існує!");
            }
            
            string[] lines = File.ReadAllLines(filePath, System.Text.Encoding.UTF8);

            Console.WriteLine($"Файл знайдено. Кількість рядків у звіті: {lines.Length}");
            Console.WriteLine("--- Перші 15 рядків звіту ---");
            for (int i = 0; i < Math.Min(15, lines.Length); i++)
            {
                Console.WriteLine(lines[i]);
            }
            Console.WriteLine("-------------------------------------");
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"ПОМИЛКА ЧИТАННЯ - {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"ПОМИЛКА I/O - Помилка введення/виведення при записі у файл: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ПРОСТО ЯКАСЬ ПОМИЛКА {ex.Message}");
        }
    }
}