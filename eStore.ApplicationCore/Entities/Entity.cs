namespace eStore.ApplicationCore.Entities
{
    public abstract class Entity
    {
        public int Id { get; protected set; }
        public bool IsDeleted { get; set; }
    }
}