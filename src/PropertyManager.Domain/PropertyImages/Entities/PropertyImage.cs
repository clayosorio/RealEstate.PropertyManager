using PropertyManager.Domain.Properties.Entities;

namespace PropertyManager.Domain.PropertyImages.Entities
{
    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public bool Enabled { get; set; }
        public required string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public required virtual Property Property { get; set; }
    }
}
