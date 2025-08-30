using PropertyManager.Domain.Owners.Entities;
using PropertyManager.Domain.PropertyImages.Entities;
using PropertyManager.Domain.PropertyTraces.Entities;

namespace PropertyManager.Domain.Properties.Entities
{
    public class Property
    {
        public int IdProperty { get; set; }
        public required string Name { get; set; }
        public required string Address { get; set; }
        public decimal Price { get; set; }
        public required string CodeInternal { get; set; }
        public int Year { get; set; }

        public int IdOwner { get; set; }
        public required virtual Owner Owner { get; set; }

        public virtual ICollection<PropertyImage>? PropertyImages { get; set; }
        public virtual ICollection<PropertyTrace>? PropertyTraces { get; set; }
    }
}
