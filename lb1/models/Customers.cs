using pr1.Base;

namespace pr1.Models;

public class Customer : Technologia
{
    public string Email { get; set; }
    public Customer(int id, string name, string email) : base(id, name,0)
    {
        Email = email;
    }
}