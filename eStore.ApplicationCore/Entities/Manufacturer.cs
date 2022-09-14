namespace eStore.ApplicationCore.Entities
{
    public class Manufacturer : Entity
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Manufacturer other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * Name.GetHashCode() * IsDeleted.GetHashCode();
            }
        }
    }
}