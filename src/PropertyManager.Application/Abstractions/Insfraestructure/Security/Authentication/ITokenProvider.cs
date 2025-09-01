using PropertyManager.Domain.Owners.Entities;

namespace PropertyManager.Application.Abstractions.Insfraestructure.Security.Authentication
{
    public interface ITokenProvider
    {
        string Create(Owner owner);
    }
}
