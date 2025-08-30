using PropertyManager.Domain.Properties.Entities;

namespace PropertyManager.Domain.Owners.Entities
{
    public class Owner
    {
        public int IdOwner { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public required string Photo { get; set; }
        public DateTime Birthday { get; set; }

        public virtual ICollection<Property>? Properties { get; set; }
    }
}
