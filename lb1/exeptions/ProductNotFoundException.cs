using System;
namespace pr1.exeptions;


public class ProductNotFoundException : Exception
{
    public int ProductId { get; }

    public ProductNotFoundException(int productId) : base($"Товар з ID {productId} не знайдено у системі.")
    {
        ProductId = productId;
    }
}