using System;
namespace pr1.Base;

public abstract class Entity
{
    public int Id { get; set; }
    protected  Entity(int Id)
    {
        this.Id = Id;
    }
}