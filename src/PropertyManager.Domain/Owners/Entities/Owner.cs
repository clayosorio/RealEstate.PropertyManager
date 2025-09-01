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
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public required string PasswordSalt { get; set; }
        public virtual ICollection<Property>? Properties { get; set; }
    }
}
