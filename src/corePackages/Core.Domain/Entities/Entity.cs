namespace Core.Domain.Entities;

public class Entity
{
    public int Id { get; set; }

    public Entity()
    {
    }

    public Entity(int id) : this()
    {
        Id = id;
    }
}