using System.Collections.Generic;
using System.Linq;

namespace pr1.Base;

public class Repository<T> where T : Entity
{
    private readonly List<T> _items = new();

    public void Add(T item)
    { 
        _items.Add(item);
    }

    public T? GetById(int id)
    {
        return _items.FirstOrDefault(x => x.Id == id);
    }

    public IReadOnlyList<T> GetAll()
    {
        return _items;
    }

    public bool Remove(int id)
    {
        var item = GetById(id);
        if (item != null)
        {
            return _items.Remove(item);
        }
        return false;
    }
}