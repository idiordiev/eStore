namespace eStore.Domain.Entities;

public abstract class Entity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
}