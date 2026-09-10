using System;
using System.Collections.Generic;
using pr1.Base;
using pr1.Models;

namespace pr2.Service;

public class ShopService
{
    private List<Technologia> _products = new List<Technologia>();
    private List<Order> _orders = new List<Order>();

    public void addProduct(Technologia product)
    {
        foreach (var Product in _products)
        {
            if (Product.Id == product.Id)
            {
                Console.WriteLine($"Продукт {product.Name} з ID {product.Id} вже існує");
                return;
            }
        }

        if (string.IsNullOrEmpty(product.Name))
        {
            Console.WriteLine($"Назва продукту не може бути порожньою");
                return;
        }
        _products.Add(product);
        Console.WriteLine($"Продукт {product.Name} з айді {product.Id} успішно додано");
    }

    public Technologia searchProducts(int id)
    {
        foreach (var product in _products)
        {
                if (product.Id == id)
                {
                    return product;
                }

        }
                return null;
    }
    
    public bool DeleteProduct(int id)
    {
        var product = searchProducts(id);
        if (product != null)
        {
            _products.Remove(product);
            Console.WriteLine($"Товар {product.Name} з Id {id} видалено.");
            return true;
        }
        Console.WriteLine($"Товар з ID {id} не знайдено для видалення.");
        return false;
    }
    
    public void CreateOrder(int orderId, Customer customer, List<int> productIds)
    {
        Order newOrder = new Order
        {
            OrderId = orderId,
            Buyer = customer
        };

        decimal total = 0;

        foreach (var id in productIds)
        {
            var product = searchProducts(id);
            if (product != null)
            {
                newOrder.Products.Add(product);
                total += product.Price;
            }
            else
            {
                Console.WriteLine($"Товар з ID {id} відсутній! Його неможливо додати в список.");
            }
        }

        if (newOrder.Products.Count == 0)
        {
            Console.WriteLine("Список товарів порожній.");
            return;
        }

        newOrder.TotalPrice = total;
        _orders.Add(newOrder);


        Console.WriteLine($"Чек замовлення {orderId}");
        Console.WriteLine($"Покупець: {customer.Name}, {customer.Email}");
        Console.WriteLine("Товари в замовленні:");
        foreach (var product in newOrder.Products)
        {
            product.GetDescription();
        }
        Console.WriteLine($"Оплата: {newOrder.TotalPrice}.");
    }
}

    
    
