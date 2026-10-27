using System;
using System.Collections.Generic;
using pr1.Base;
using pr1.exeptions;
using pr1.Models;

namespace pr2.Service;

public class ShopService
{
    private readonly Repository<Technologia> _productRepository = new();
    private readonly Repository<Order> _orderRepository = new();

    //добавис ексепшнс
    public void AddProduct(Technologia product)
    {
        if (Validation.IsEmpty(product.Name))
        {
            throw new InvalidProductDataException("Назва товару не може бути порожньою!");
        }

        if (!Validation.IsPositivePrice(product.Price))
        {
            throw new InvalidProductDataException($"Ціна товару '{product.Name}' не може бути від'ємною!");
        }

        if (_productRepository.GetById(product.Id) != null)
        {
            throw new InvalidProductDataException($"Товар з ID {product.Id} вже існує у системі!");
        }

        _productRepository.Add(product);
        Console.WriteLine($"Продукт '{product.Name}' з ID: {product.Id} - успішно додано.");
    }

    //метод через Result<T>
    public Result<Technologia> TryGetProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return Result<Technologia>.Failure($"Товар з ID {id} відсутній у каталозі!");
        }
        return Result<Technologia>.Success(product);
    }
    
    public Result<Technologia> TryDeleteProduct(int id)
    {
        var product = _productRepository.GetById(id);
        if (product == null)
        {
            return Result<Technologia>.Failure($"Товар з ID {id} відсутній у каталозі!");
        }

        _productRepository.Remove(id);
        Console.WriteLine($"Товар '{product.Name}' з ID {id} успішно видалено");

        return Result<Technologia>.Success(product);
    }
    
    //замовлення через Result<T>
    public Result<Order> TryCreateOrder(Customer customer, List<int> productIds)
    {
        if (customer == null)
        {
            return Result<Order>.Failure("Неможливо створити замовлення без покупця");
        }

        List<Technologia> validProducts = new();
        foreach (var id in productIds)
        {
            var productResult = TryGetProduct(id);
            if (productResult.IsSuccess && productResult.Value != null)
            {
                validProducts.Add(productResult.Value);
            }
            else
            {
                Console.WriteLine($"{productResult.ErrorMessage}");
            }
        }

        if (validProducts.Count == 0)
        {
            return Result<Order>.Failure("Замовлення не сформовано: жодного з вказаних товарів немає у наявності.");
        }

        int orderId = IdGenerator.NextId();
        Order newOrder = new Order(orderId, customer)
        {
            Products = validProducts
        };

        decimal total = 0;
        foreach (var p in validProducts) total += p.Price;
        newOrder.TotalPrice = total;
        
        Console.WriteLine($"\n--- Чек замовлення N{orderId} ---");
        Console.WriteLine($"Покупець: {customer.Name} - {customer.Email} - {customer.Id}");
        Console.WriteLine("Товари:");
        foreach (var p in newOrder.Products)
        {
            p.GetDescription();
        }
        Console.WriteLine($"Загальна сума: {newOrder.TotalPrice} грн\n");
        _orderRepository.Add(newOrder);
        return Result<Order>.Success(newOrder);
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

    public void GenerateReport(string fileName)
    {
        ReportService.ExportShopReport(fileName, _productRepository, _orderRepository);
    }
}