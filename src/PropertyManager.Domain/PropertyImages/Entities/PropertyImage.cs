using PropertyManager.Domain.Properties.Entities;

namespace PropertyManager.Domain.PropertyImages.Entities
{
    public class PropertyImage
    {
        public int IdPropertyImage { get; set; }
        public int IdProperty { get; set; }
        public required string File { get; set; }
        public bool Enabled { get; set; }

        public required virtual Property Property { get; set; }
    }
}
