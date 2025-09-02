namespace PropertyManager.Domain.Properties.Queries.Output
{
    public class PropertyOutputDto
    {
        public int IdProperty { get; set; }
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public decimal Price { get; set; }
        public string CodeInternal { get; set; } = default!;
        public int Year { get; set; }
        public int IdOwner { get; set; }
        public string OwnerName { get; set; } = default!;
        public DateTime? CreatedAt { get; set; }
    }
}
