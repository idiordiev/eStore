namespace eStore.ApplicationCore.Entities
{
    public class Backlight : Entity
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Backlight other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.Name == other.Name;
            }

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