using PropertyManager.Domain.Properties.Entities;

namespace PropertyManager.Domain.PropertyTraces.Entities
{
    public class PropertyTrace
    {
        public int IdPropertyTrace { get; set; }
        public DateTime DateSale { get; set; }
        public required string Name { get; set; }
        public decimal Value { get; set; }
        public decimal Tax { get; set; }
        public int IdProperty { get; set; }
        public DateTime CreatedAt { get; set; }
        public required virtual Property Property { get; set; }
    }
}
