using pr1.Base;

namespace pr1.Models;

public class Customer : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }

    public Customer(int id, string name, string email) : base(id)
    {
        Name = name;
        Email = email;
    }
}