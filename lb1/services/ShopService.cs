using System;
using System.Collections.Generic;
using pr1.Base;
using pr1.Models;

namespace pr2.Service;

public class ShopService
{
    private readonly Repository<Technologia> _productRepository = new();
    private readonly Repository<Order> _orderRepository = new();

    public void AddProduct(Technologia product)
    {
        if (Validation.IsEmpty(product.Name))
        {
            Console.WriteLine("Назва продукту не може бути порожньою!");
            return;
        }

        if (_productRepository.GetById(product.Id) != null)
        {
            Console.WriteLine($"Продукт з ID {product.Id} вже існує!");
            return;
        }

        if (!Validation.IsPositivePrice(product.Price))
        {
            Console.WriteLine($"Ціна в товара {product.Name} з ID: {product.Id} - не може бути від'ємною!");
            return;
        }
        _productRepository.Add(product);
        Console.WriteLine($"Продукт '{product.Name}', ID: {product.Id} додано");
    }

    public Technologia? SearchProduct(int id)
    {
       return _productRepository.GetById(id);
    }
    
    
    public bool DeleteProduct(int id)
    {
        bool removed = _productRepository.Remove(id);
        if (removed)
        {
            Console.WriteLine($"Товар з ID {id} успішно видалено");
        }
        else{
            Console.WriteLine($"Товар з ID {id} не знайдено");
        }
        return removed;
    }

    public void PrintAllProducts()
    {
        var products = _productRepository.GetAll();

        if (products.Count == 0)
        {
            Console.WriteLine("Список порожній - товарів немає");
            return;
        }

        Console.WriteLine("----- Список товарів -----");
        foreach (var product in products)
        {
            product.GetDescription();
        }
        Console.WriteLine("---------------------------\n");
    }
    
    public void CreateOrder(Customer customer, List<int> productIds)
    {
        int orderId = IdGenerator.NextId();
        Order newOrder = new Order(orderId, customer);
        decimal total = 0;

        foreach (var id in productIds)
        {
            var product = _productRepository.GetById(id);
            if (product != null)
            {
                newOrder.Products.Add(product);
                total += product.Price;
            }
            else
            {
                Console.WriteLine($"Товар з ID {id} відсутній!");
            }
        }

        if (newOrder.Products.Count == 0)
        {
            Console.WriteLine("Не вдалося створити замовлення: список товарів порожній.");
            return;
        }

        newOrder.TotalPrice = total;
        _orderRepository.Add(newOrder);

        Console.WriteLine($"\n--- Чек замовлення N{orderId} ---");
        Console.WriteLine($"Покупець: {customer.Name} - {customer.Email} - {customer.Id}");
        Console.WriteLine("Товари:");
        foreach (var p in newOrder.Products)
        {
            p.GetDescription();
        }
        Console.WriteLine($"Загальна сума: {newOrder.TotalPrice} грн\n");
    }
}